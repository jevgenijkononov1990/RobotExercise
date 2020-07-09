using Microsoft.Extensions.Options;
using Robot.Common.Models;
using Robot.Infrastructure.Settings;
using System;

namespace Robot.ConsoleApp.Helpers
{
    public class RobotRepoSettings : IRobotRepoSettings
    {
        private readonly IOptions<RobotConfig> _settings;

        public RobotRepoSettings(IOptions<RobotConfig> settings)
        {
            //defense
            _settings = settings ?? throw new ArgumentNullException("RobotRepoSettings init failure due to iotions");
        }

        public RobotConfig GetEnviromentSettings()
        {
            return _settings == null || _settings?.Value == null ? null : _settings.Value;
        }
    }
}
