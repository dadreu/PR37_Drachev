using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop_Drachev.Data.Models;
using Shop_Drachev.Data.Interfaces;

using Shop_Drachev.Data.DataBase;

namespace Shop_Drachev
{
    public class Startup
    {
        public static List<ItemsBasket> BasketItem = new List<ItemsBasket>();
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICategorys, DBCategory>();
            services.AddTransient<IItems, DBItems>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
