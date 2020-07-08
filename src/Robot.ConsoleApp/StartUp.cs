using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Robot.Common.Logging;

namespace Robot.ConsoleApp
{
    public static class StartUp
    {
        private static IServiceCollection CreateServices()
        {
            var service = new ServiceCollection();

            RegisterService(service);

            return service;
        }

        public static ServiceProvider BuildServiceProvider()
        {
            return CreateServices().BuildServiceProvider();
        }

        private static void RegisterService(IServiceCollection services)
        {

        }
    }
}
