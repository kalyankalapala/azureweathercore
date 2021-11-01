using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        //// Azure Key Vault ////
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //services.AddScoped<InquiryService>();
            services.AddHttpClient<WeatherClient>(httpClient =>
            {
                //include the backend application url once it is deployed to appservice
                //httpClient.BaseAddress = new Uri("https://weatherap.azurewebsites.net");
                httpClient.BaseAddress = new Uri(Configuration.GetValue<string>("WeatherAPIURL"));
            });
        }


        // public void ConfigureServices(IServiceCollection services)
        // {
        //     services.AddRazorPages();
        //     services.AddHttpClient<WeatherClient>(httpClient =>
        //     {
        //         //include the backend application url once it is deployed to appservice
        //         httpClient.BaseAddress = new Uri("https://weatherap.azurewebsites.net");
        //     });
        // }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
