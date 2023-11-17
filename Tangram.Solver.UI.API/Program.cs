using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Tangram.Solver.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel()
                    .UseStartup<Startup>();
                    //.UseUrls("http://[::];http://+:80;https://+:443"); //WIP
                });
    }
}
