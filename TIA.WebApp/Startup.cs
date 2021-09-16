using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogic;
using TIA.BusinessLogicBase.Abstractions;
using TIA.EntityFramework;
using TIA.EntityFramework.Services;

namespace TIA.WebApp
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
            services.AddControllersWithViews();
            

            services.AddSingleton(typeof(TIADbContextFactory));
            services.AddSingleton<ICatalogDataService, CatalogDataService>();
            services.AddSingleton<IProductDataService, ProductDataService>();
            services.AddSingleton<ITiaModel, TiaModel>();

            services.AddMvc(st=> {

                st.EnableEndpointRouting = false;
            
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapControllerRoute(
            //        name: "product",
            //        pattern: "{controller=Product}/{action=Index}/{id?}");
            //    endpoints.MapControllerRoute(
            //       name: "product",
            //       pattern: "{controller=Catalog}/{action=Index}/{id?}");
            //});

            app.UseMvc(routes => {

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
                //routes.MapRoute(
                //    name: "product",
                //    template: "{controller=Product}/{action=Index}/{id?}");
                //routes.MapRoute(
                //   name: "catalog",
                //   template: "{controller=Catalog}/{action=Index}");

            });
        }
    }
}
