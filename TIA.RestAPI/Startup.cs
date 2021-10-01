using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogicLayer;
using TIA.BusinessLogicLayerBase.Abstractions;
using TIA.DataAccessLayer.Models;
using TIA.DataAccessLayer.Repositories;
using TIA.RestAPI.JwtLogic;

namespace TIA.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(typeof(TIA_DBContext), (s) => s.GetRequiredService<TIA_DBContextFactory>().CreateDbContext(null));

            services.AddSingleton(typeof(TIA_DBContextFactory));
            services.AddSingleton<ICatalogRepository, CatalogRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ITiaModel, TiaModel>();

            services.AddMvc(st =>
            {
                st.EnableEndpointRouting = false;
            }).AddNewtonsoftJson();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TIA_DBContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // ��������, ����� �� �������������� �������� ��� ��������� ������
                            ValidateIssuer = true,
                            // ������, �������������� ��������
                            ValidIssuer = AuthOptions.ISSUER,

                            // ����� �� �������������� ����������� ������
                            ValidateAudience = true,
                            // ��������� ����������� ������
                            ValidAudience = AuthOptions.AUDIENCE,
                            // ����� �� �������������� ����� �������������
                            ValidateLifetime = true,

                            // ��������� ����� ������������
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // ��������� ����� ������������
                            ValidateIssuerSigningKey = true,
                        };
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc();
        }
    }
}
