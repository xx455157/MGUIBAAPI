#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Models;
using GUIStd;

#endregion

namespace MGUIBAAPI.Features
{
    /// <summary>
    /// API 網站服務層共用函式庫
    /// </summary>
    public class Utils
    {
        #region " 共用屬性 "

        /// <summary>
        /// 取得 JSON 設定檔中的 AppSettings 區塊
        /// </summary>
        public static MdApiAppSettings AppSettings => Config<MdApiAppSettings>.AppSettings;

        /// <summary>
        /// 取得所需的金旭組件 XML 檔案
        /// </summary>
        public static List<string> GUIAssemblyXMLFiles
        {
            get
            {
                if (mGUIAssemblyXMLFiles == null)
                {
                    var _xmlFiles = new List<string>
                    {
                        // 加入專案 API 註解 XML 檔案
                        Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")
                    };

                    // 加入所有 GUIStd.DAL.*.dll 註解 XML 檔案到 Swagger 中
                    foreach (var _file in Directory.GetFiles(AppContext.BaseDirectory, "GUIStd.DAL.*.xml"))
                    {
                        _xmlFiles.Add(_file);
                    };

                    // 加入 GUIStd.dll 註解 XML 檔案到 Swagger 中
                    foreach (var _file in Directory.GetFiles(AppContext.BaseDirectory, "GUIStd.xml"))
                    {
                        _xmlFiles.Add(_file);
                    };

                    // 加入 GUICore.Web.dll 註解 XML 檔案到 Swagger 中
                    _xmlFiles.Add(Path.Combine(AppContext.BaseDirectory, "GUICore.Web.xml"));

                    mGUIAssemblyXMLFiles = _xmlFiles;
                }

                return mGUIAssemblyXMLFiles;
            }
        }
        private static List<string> mGUIAssemblyXMLFiles = null;

        #endregion

        #region " 共用函式 "

        /// <summary>
        /// 啟用 Swagger UI 文件產生器中介軟體
        /// </summary>
        /// <param name="app">IApplicationBuilder 物件</param>
        public static void UseSwaggerUi(IApplicationBuilder app)
        {
            // 若設定不顯示輔助說明文件，則不執行下方啟用中介軟體的程式碼
            if (!Config.AppSettings.ShowAPIHelp) return;

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                var _provider = app.ApplicationServices.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
                foreach (var desc in _provider.ApiDescriptionGroups.Items.OrderBy(x => x.GroupName))
                {
                    var _groupText = desc.GroupName;

                    // 依群組名稱設定其 swagger.json 檔案的 URL
                    var _basePath = string.IsNullOrWhiteSpace(config.RoutePrefix) ? "." : "..";
                    config.SwaggerEndpoint($"{_basePath}/swagger/{_groupText}/swagger.json", _groupText);
                }
            });
        }

        /// <summary>
        /// 註冊 Swagger 文件產生器服務
        /// </summary>
        /// <param name="services">IServiceCollection 物件</param>
        public static void AddSwaggerService(IServiceCollection services)
        {
            // 若設定不顯示輔助說明文件，則不執行下方註冊服務的程式碼
            if (!Config.AppSettings.ShowAPIHelp) return;

            services.AddSwaggerGen(options =>
            {
                var _provider = services.BuildServiceProvider().GetService<IApiDescriptionGroupCollectionProvider>();

                foreach (var desc in _provider.ApiDescriptionGroups.Items.OrderByDescending(item => item.GroupName))
                {
                    options.SwaggerDoc(desc.GroupName, new OpenApiInfo
                    {
                        Version = Utils.AppSettings.ApiHelp.Version,
                        Title = Utils.AppSettings.ApiHelp.Title,
                        Description = Utils.AppSettings.ApiHelp.Description,
                        TermsOfService = null,
                        Contact = new OpenApiContact
                        {
                            Name = AppSettings.CompanyInfo.ServiceName,
                            Email = AppSettings.CompanyInfo.ServiceMail,
                            Url = new Uri(AppSettings.CompanyInfo.HomePage)
                        }
                    });
                }

                // 使用完整命名空間自訂模型的 Schema 名稱
                options.CustomSchemaIds(type =>
                {
                    // 處理泛型型別
                    // Schema Name = 包含名稱空間的完整模型類別名稱[泛型參數一包含名稱空間的完整模型類別名稱]
                    if (type.IsGenericType)
                    {
                        var _tName = type.GetGenericArguments()
                            .Select(t => t.FullName)
                            .First();

                        return $"{type.Namespace}.{type.Name.Split('`')[0]}[{_tName}]";
                    }

                    // 處理一般型別
                    // Schema Name = 包含名稱空間的完整模型類別名稱
                    // * 避免 "Enums+ReportType" 即 + 符號導致以下錯誤，將 + 以 . 替換
                    //   Resolver error at components.schemas.GUIStd.DAL.Base.Models.Reports.MdReportQuery[GUIStd.DAL.GUI.Models.Private.vSCR01.MdSCR01_q].properties.basic.properties.reportType.$ref
                    //   Could not resolve reference: Could not resolve pointer: / components/schemas/GUIStd.Enums+ReportType does not exist in document
                    return type.FullName.Replace("+", ".");
                });

                // 將 Swagger UI 列舉值下拉的資料顯示，由僅數值改為數值+名稱的顯示
                options.SchemaFilter<EnumSchemaFilter>();

                // 加入金旭組件註解 XML 檔案到 Swagger 中
                foreach (var _xml in GUIAssemblyXMLFiles)
                {
                    if (File.Exists(_xml)) options.IncludeXmlComments(_xml);
                }
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        #endregion
    }
}