using Microsoft.Extensions.Options;
using Moq;
using Robot.Common.Models;
using Robot.ConsoleApp.Helpers;
using System;
using Xunit;

namespace Robot.Tests.RepoTests
{
    public class RobotRepoSettingsTests
    {
        public Mock<IOptions<RobotConfig>> _mockOptions;
        public RobotRepoSettings _robotRepository;


        public RobotRepoSettingsTests()
        {
            _mockOptions = new  Mock<IOptions<RobotConfig>>();

            _robotRepository = new RobotRepoSettings(_mockOptions.Object);
        }
        [Fact]
        public void Test_Robot_Repository_Constructor_ForDefense_When_Dependency_Null_Result_ThrowException()
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {
                new RobotRepoSettings(null);
            });

            //Arrange
            Assert.Throws<ArgumentNullException>(arrange);
        }

        [Fact]
        public void Test_Robot_Repository_GetSettings_When_OptionsNullOrValueNull_ResultNull()
        {
            //Arrange

          
            //Act
            var result = _robotRepository.GetEnviromentSettings();

            //Arrange
            Assert.Null(result);
        }



        //to finish
    }
}
