using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using Api;

namespace API
{
    /// <summary>
    /// Configuration
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc();
            services.AddMediatR(AppDomain.CurrentDomain.Load("Application"));
            IoCConfiguration.Configure(services);
        }        
    }
}
