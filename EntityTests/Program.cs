using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}
//var host = Host
//    .CreateDefaultBuilder(args)
//    .ConfigureServices((context, services) =>
//    {
//        var configuration = context.Configuration;
//        services.AddDbContext<MyContext>(options =>
//            options.UseSqlServer(configuration.GetConnectionString("someConnection")));
//    })
//    .Build();

//using (var ctx = host.Services.GetRequiredService<MyContext>())
//{
//    var cnt = await ctx.Customers.CountAsync();
//    Console.WriteLine(cnt);
//}            
//}