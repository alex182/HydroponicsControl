using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using HydroPiApi.Controllers.Common.Processor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RelayClient;
using RelayClient.Models;

namespace HydroPiApi
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

            var relays = Configuration.GetSection("Relays").Get<List<Relay>>();
            var relayOptions = new RelayClientOptions
            {
                Relays = relays
            };

            //TODO: use a real logger eventually
            var loggerFactory = new LoggerFactory();

            services.AddTransient(provider => loggerFactory);

            if (Environment.MachineName == devMachineName)
            {
                services.AddSingleton<IGpioController, MockGpioDriver>();
            }
            else
            {
                services.AddSingleton<IGpioController, GpioController>();
            }

            services.AddSingleton<IRelayClientOptions>(provider => relayOptions);
            services.AddSingleton<IRelayClient, RelayClient.RelayClient>();
            services.AddTransient<IProcessorFactory, ProcessorFactory>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
