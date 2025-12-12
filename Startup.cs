#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web;
using GUICore.Web.Filters;
using GUICore.Web.Middlewares;
using GUICore.Web.Models;
using MGUIBAAPI.Features;
using MGUIBAAPI.Hubs.HTLPRE;

#endregion

namespace MGUIBAAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            // 初始化配置檔中的應用程式組態設定物件
            App<MdApiAppSettings>.InitConfig(configuration);

            // 註冊 Aspose.Cells 的 License
            GUIStd.Excel.Base.SetLicense();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 停止 Invalid ModelState Filter，關閉後模型驗證失敗會沒反應
            services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

			services.AddMvc(
				options =>
				{
					// 加入自訂 Validate Model Filter，執行模型驗證失敗的處理
					options.Filters.Add(typeof(ValidateModelFilter));

                    // 在 MVC 服務中新增 ApiExplorerVersionConvention 約定
                    options.Conventions.Add(new ApiExplorerVersionConvention());
				}
			).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // 啟用跨原始要求 (Enabling Cross-Origin Requests) 服務, 可用來限制可存取的網域
            services.AddCors(
                options => options.AddPolicy(
                    "AllowCors",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().
                        WithExposedHeaders("Content-Disposition")
                )
            );

			//加入 Signalr 服務
            services.AddSignalR(hubOptions =>
            {
                hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(60);
            });

			// 註冊 Swagger 文件產生器服務
			Utils.AddSwaggerService(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Initialize JSON File Logging Settings
            // 初始化配置檔中的記錄組態設定物件
            App<MdApiAppSettings>.InitLogging(loggerFactory);

            // 強制使用 HTTPS
            app.UseHsts();

            // 使用自訂請求中介軟體元件
            app.UseCustomRequest();

			// 使用 JSON 格式例外中介軟體元件
			app.UseJsonException();

			// 加入 Cors Middleware 到 Request Pipeline, 以允許跨網域要求
			app.UseCors("AllowCors");

			// 啟用 Swagger 文件產生器中介軟體元件
			Utils.UseSwaggerUi(app);

            app.UseCookiePolicy();
            app.UseStaticFiles();
            
            // 將沒有副檔名的檔案視為圖片
            //   - 此為使用網站內的圖片時，IIS、Web Azure 預設不支援沒有副檔名的檔案
            //   - 可考慮將圖片放至 Azure Blob 上，支援沒有副檔名的檔案，不需下一行程式碼
            //app.UseCustomUnknownFileType("image/png", "/images", "images");

            // 允許瀏覽圖片資料夾
            //app.UseCustomDirectoryBrowsing("/images", "images");

			// 設定SignalR路由
            app.UseSignalR(options =>
            {
                options.MapHub<QueueHub>("/htlpre/queue");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
