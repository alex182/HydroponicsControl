using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RelayClient;
using RelayClient.Models;

namespace HydroponicsControl
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //TODO: get this off the raspi from an environmental variable
            var relays = new List<Relay>();
            var relayOptions = new RelayClientOptions
            {
                Relays = relays
            };


            //TODO: use a real logger eventually
            var loggerFactory = new LoggerFactory()
              .AddConsole()
              .AddDebug();

            ILogger logger = loggerFactory.CreateLogger<Program>();

            services.AddTransient<ILogger>(provider => logger);
            services.AddSingleton<IGpioController, GpioController>();
            services.AddSingleton<IRelayClientOptions>(provider => relayOptions); 
            services.AddSingleton<IRelayClient,RelayClient.RelayClient>(); 

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",new Swashbuckle.AspNetCore.Swagger.Info { Title ="HydroPi", Version ="V1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HydroPi");
           
            });

            app.UseMvc();
        }
    }
}
