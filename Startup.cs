using BenriShop.ApiRepository.Accounts;
using BenriShop.ApiRepository.CartItems;
using BenriShop.ApiRepository.OrderItems;
using BenriShop.ApiRepository.Orders;
using BenriShop.ApiRepository.Products;
using BenriShop.Models;
using BenriShop.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenriShop
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
            var connection = Configuration.GetConnectionString("BenriShopDatabase");
            services.AddControllersWithViews();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            //services.AddDbContextPool<InventoryContext>(options => options.UseSqlServer(connection));
            services.AddHttpClient();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login";
            //    options.AccessDeniedPath = "/User/Forbidden/";
            //});

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //});

            services.AddDbContext<BenriShopContext>(op => op.UseSqlServer(connection));

            services.AddControllers();

            // Rest of the code

            /*We are using AddScoped() method because we want the instance 
             * to be alive and available for the entire scope of the given HTTP request.
             * For another new HTTP request, a new instance of EmployeeRepository class will 
             * be provided and it will be available throughout the entire scope of that HTTP request.
            */
           services.AddScoped<IAccountRepository, AccountRepository>();
           services.AddScoped<IProductsRepository, ProductsRepository>();
           services.AddScoped<ICartItemRepository, CartItemRepository>();
           services.AddScoped<IOrderItemRepository, OrderItemRepository>();
           services.AddScoped<IOrderRepository, OrderRepository>();
          



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);
#pragma warning restore CS0618 // Type or member is obsolete
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
