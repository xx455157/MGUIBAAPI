#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

#endregion

namespace MGUIBAAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
