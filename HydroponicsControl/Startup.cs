using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using HydroponicsControl.Controllers.Common.Processor;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Request;
using HydroponicsControl.Controllers.Relay.Version1.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RelayClient;
using RelayClient.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;

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
            var devMachineName = Configuration.GetValue<string>("DevMachineName");
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var relays = Configuration.GetSection("Relays").Get<List<Relay>>();
            var relayOptions = new RelayClientOptions
            {
                Relays = relays
            };

            
            //TODO: use a real logger eventually
            var loggerFactory = new LoggerFactory();

            services.AddTransient<IValidator<GetRelayStateProcessorRequestVersionOne>, GetRelayStateRequestValidator>();
            services.AddTransient(provider => loggerFactory);

            if(Environment.MachineName == devMachineName)
            {
                services.AddSingleton<IGpioController, MockGpioDriver>();
            }
            else
            {
                services.AddSingleton<IGpioController, GpioController>();
            }

            services.AddSingleton<IRelayClientOptions>(provider => relayOptions);
            services.AddSingleton<IRelayClient, RelayClient.RelayClient>();
            services.AddTransient<IProcessorFactory,ProcessorFactory>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation();


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
