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
using TrolleyApi.TrolleyTotal;
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
                .AddTransient<IProductSortService, SortByName>()
                .AddTransient<IProductSortService, SortByRecommendation>()
                .AddTransient<ISpecialsProcessor, SpecialsProcessor>()
                .AddTransient<INormalPriceProcessor, NormalPriceProcessor>()
                .AddTransient<ITrolleyTotalCalculatorService, TrolleyTotalCalculatorService>();

            services
                .AddTransient<ISortService>(sp => 
                {
                    var sortServices = sp.GetServices(typeof(IProductSortService)) 
                        as IEnumerable<IProductSortService>;
                    var productRepository = sp.GetRequiredService<IProductsRepository>();
                    return new SortService(Configuration, productRepository, sortServices.ToList());
                });

            var resourceApiBaseUrl = new Uri(Configuration["ResourceApiBaseUrl"]);

            services
                .AddHttpClient("ProductsRepository", c =>
                {
                    c.BaseAddress = resourceApiBaseUrl;
                })
                .AddTypedClient(c => Refit.RestService.For<IProductsRepository>(c));

            services
                .AddHttpClient("ShopperHistoryRepository", c =>
                {
                    c.BaseAddress = resourceApiBaseUrl;
                })
                .AddTypedClient(c => Refit.RestService.For<IShopperHistoryRepository>(c));
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
