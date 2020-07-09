using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Robot.Common.Models;
using Robot.ConsoleApp.Helpers;

using Robot.Infrastructure.Communication;
using Robot.Infrastructure.RobotService;
using Robot.Infrastructure.Settings;
using Robot.Infrastructure.StateMachine;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Robot.ConsoleApp
{
    public class ValidatableOptionsFactory<TOptions> : OptionsFactory<TOptions> where TOptions : class, new()
    {
        private static readonly string NamespacePrefix = typeof(Program).Namespace.Split('.').First();

        public ValidatableOptionsFactory(IEnumerable<IConfigureOptions<TOptions>> setups, IEnumerable<IPostConfigureOptions<TOptions>> postConfigures)
                : base(setups, postConfigures)
        {
            if (typeof(TOptions).Namespace.StartsWith(NamespacePrefix) && setups.Any() == false && postConfigures.Any() == false)
            {
                throw new InvalidOperationException($"No configuration options or post configuration options was found to configure {typeof(TOptions)}");
            }
        }
    }

    public static class StartUp
    {
        public static ServiceProvider BuildServiceProvider()
        {
            return CreateServices().BuildServiceProvider();
        }

        #region Dependency Injection Initialization

        private static IServiceCollection CreateServices()
        {
            var service = new ServiceCollection();

            string evn = "development";
#if DEBUG

#else
                evn = ""
#endif

            RegisterService(service, evn);

            return service;
        }

        private static void RegisterService(IServiceCollection services, string environmentName)
        {
            var jsonFile = $"appsettings.{environmentName}.json";
            if (jsonFile.Contains(".."))
            {
                jsonFile.Replace("..", "");
            }

            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                //              .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(jsonFile, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = configBuilder.Build();

            services.AddTransient(typeof(IOptionsFactory<>), typeof(ValidatableOptionsFactory<>));
            services.AddSingleton<IConfiguration>(configuration);
            services.Configure<RobotConfig>(configuration.GetSection("RobotConfig"));
            services.AddOptions();

            services.AddSingleton<ICommunicationHandler, CommunicationHandler>();
            services.AddSingleton<IRobotRepoSettings, RobotRepoSettings>();
            services.AddSingleton<ICommunicationHandler, CommunicationHandler>();
            services.AddSingleton<IMainRobotService, MainRobotService>();
            services.AddSingleton<IRobotStateMachineFactory, RobotStateMachineFactory>();
            services.AddSingleton<IRobotLocationService, RobotLocationService>();
        
            #endregion
        }
    }
}
