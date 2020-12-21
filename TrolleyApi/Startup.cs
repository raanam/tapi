using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.ApiProxies;
using TrolleyApi.Sort;
using TrolleyApi.User;

namespace TrolleyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IProductSortService, SortByPrice>()
                .AddTransient<IProductSortService, SortByName>();

            services
                .AddTransient<ISortService>(sp => 
                {
                    var sortServices = sp.GetServices(typeof(IProductSortService)) 
                        as IEnumerable<IProductSortService>;
                    var productRepository = sp.GetRequiredService<IProductsRepository>();
                    return new SortService(Configuration, productRepository, sortServices.ToList());
                });

            services
                .AddHttpClient("ProductsRepository", c =>
                {
                    c.BaseAddress = new Uri("http://dev-wooliesx-recruitment.azurewebsites.net/api/resource");
                })
                .AddTypedClient(c => Refit.RestService.For<IProductsRepository>(c));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
