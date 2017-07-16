using Dora.Interception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Demo2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Configure(app => app.UseMvc())
                .Build()
                .Run();
        }

        public class Startup
        {
            public IServiceProvider ConfigureServices(IServiceCollection services)
            {
                return services
                    .AddSingleton<ISystomClock, SystomClock>()
                    .Configure<MemoryCacheEntryOptions>(options => options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(2))
                    .BuilderInterceptableServiceProvider(builder => builder.SetDynamicProxyFactory());
            }

            public void Configure(IApplicationBuilder app)
            {
                app.UseMvc();
            }
        }
    }
}
