using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TIA.BusinessLogic;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.AspNetCoreEntities;
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
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TIADbContext>();

            services.AddSingleton(typeof(TIADbContext), (s) => s.GetRequiredService<TIADbContextFactory>().CreateDbContext(null));

            services.AddSingleton(typeof(TIADbContextFactory));
            services.AddSingleton<ICatalogDataService, CatalogDataService>();
            services.AddSingleton<IProductDataService, ProductDataService>();
            services.AddSingleton<ITiaModel, TiaModel>();

            services.AddMvc(st =>
            {
                st.EnableEndpointRouting = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //       name: "default",
            //       template: "{controller=Home}/{action=Index}");
            //});
        }
    }
}
