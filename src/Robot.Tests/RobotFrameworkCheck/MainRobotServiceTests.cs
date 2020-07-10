using Moq;
using Robot.Common.Enms;
using Robot.Common.Models;
using Robot.Infrastructure.Communication;
using Robot.Infrastructure.RobotService;
using Robot.Infrastructure.Settings;
using Robot.Infrastructure.StateMachine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Robot.Tests.RobotFrameworkCheck
{
    public enum TestOptions {

        NULL_RETURN,
        THROWS_EXCEPTION,
        NULL_Config_MatrixSize,
        NULL_Config_Position,
        ProcessState,
        ALL_SUCCESS,
    }


    public class MainRobotServiceTests
    {
        private readonly Mock<ICommunicationHandler> _communicationHandlerMock;
        private readonly Mock<IRobotRepoSettings> _robotSettingsMock;
        private readonly Mock<IRobotStateMachineFactory> _robotStateMachineMock;

        private MainRobotService _mainRobotService;

        public MainRobotServiceTests()
        {
            _communicationHandlerMock = new Mock<ICommunicationHandler>();
            _robotSettingsMock = new Mock<IRobotRepoSettings>();
            _robotStateMachineMock = new Mock<IRobotStateMachineFactory>();
            _mainRobotService = new MainRobotService(_communicationHandlerMock.Object, _robotSettingsMock.Object, _robotStateMachineMock.Object);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 1, 1)]
        public void MainRobotService_Constructor_ForDefense_When_Dependency_Null_Result_ThrowException(int initServer1, int initServer2, int initServer3)
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {
                new MainRobotService
                (
                    initServer1 != 0 ? new Mock<ICommunicationHandler>().Object : null,
                    initServer2 != 0 ? new Mock<IRobotRepoSettings>().Object : null,
                    initServer3 != 0 ? new Mock<IRobotStateMachineFactory>().Object : null
                );
            });

            //Arrange
            Assert.Throws<ArgumentNullException>(arrange);
        }

        [Theory]
        [InlineData(TestOptions.NULL_RETURN)]
        [InlineData(TestOptions.THROWS_EXCEPTION)]
        [InlineData(TestOptions.NULL_Config_MatrixSize)]
        [InlineData(TestOptions.NULL_Config_Position)]
        public void MainRobotService_OsInitialization_Failure_When_GetEnviromentNullOrSomethingIsWrong_Result_False(TestOptions exceptionOption)
        {
            //Arrange

            RobotConfig config = new RobotConfig
            {
                Position = new RobotPosition(),
                MatrixSize = new MatrixSize()
            };

            switch (exceptionOption)
            {
                case TestOptions.NULL_RETURN:
                    _robotSettingsMock.Setup(x => x.GetEnviromentSettings()).Returns((RobotConfig)null);
                    break;
                case TestOptions.THROWS_EXCEPTION:
                    _robotSettingsMock.Setup(x => x.GetEnviromentSettings()).Throws<Exception>();
                    break;
                case TestOptions.NULL_Config_MatrixSize:
                    config.MatrixSize = null;
                    _robotSettingsMock.Setup(x => x.GetEnviromentSettings()).Returns(config);
                    break;
                case TestOptions.NULL_Config_Position:
                    _robotSettingsMock.Setup(x => x.GetEnviromentSettings()).Returns(config);
                    config.Position = null;
                    break;
            }

            //Act
            bool result = _mainRobotService.OsInitialization();

            //Arrange
            Assert.False(result);
        }

        private (bool success, StateResponse response) DemoResult(bool resultValue = false, bool initObject = false)
        {
            return (resultValue, initObject == true? new StateResponse(): null);
        }

        [Fact]
        public void MainRobotService_OsInitialization_Success_When_RobotStateMachine_InitComplete_Result_True()
        {
            //Arrange
            RobotConfig config = new RobotConfig
            {
                Position = new RobotPosition(),
                MatrixSize = new MatrixSize()
            };
            _robotSettingsMock.Setup(x => x.GetEnviromentSettings()).Returns(config);
            _robotStateMachineMock.Setup(x => x.Build(It.IsAny<RobotCommandType>()).ProcessState(It.IsAny<RobotCommand>(),It.IsAny<MatrixSize>())).Returns(DemoResult(true,true));

            //Act
            bool result = _mainRobotService.OsInitialization();

            //Arrange
            Assert.True(result);
        }

        [Fact]
        public void MainRobotService_OsInitialization_Failure_When_RobotStateMachine_InitFailure_Result_False()
        {
            //Arrange
            RobotConfig config = new RobotConfig
            {
                Position = new RobotPosition(),
                MatrixSize = new MatrixSize()
            };
            _robotSettingsMock.Setup(x => x.GetEnviromentSettings()).Returns(config);
            _robotStateMachineMock.Setup(x => x.Build(It.IsAny<RobotCommandType>()).ProcessState(It.IsAny<RobotCommand>(), It.IsAny<MatrixSize>())).Returns(DemoResult(false, false));

            //Act
            bool result = _mainRobotService.OsInitialization();

            //Arrange
            Assert.False(result);
        }
        
        [Fact]
        public async Task MainRobotService_Failure_When_StartCommunicationThreadAsync_FailsBecauseOf_CancelationTask_NUll()
        {
            //Arrange

            //act
            Func<Task> act = async () => await _mainRobotService.StartCommunicationThreadAsync(null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }
    }
}
