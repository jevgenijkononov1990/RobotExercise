using Moq;
using Robot.Common.Models;
using Robot.Infrastructure.Communication;
using Robot.Infrastructure.Communication.Decoders;
using Robot.Infrastructure.StateMachine.LocationHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Robot.Tests
{
    public class CommunicationHandlerTests
    {
        //to fnish...
        private Mock<ILocationHelperProcessor> _locationHelperProcessorMock;
        private Mock<IDecoder> _decoderServiceMock;
        private CommunicationHandler communicationHandler;

        public CommunicationHandlerTests()
        {
            _locationHelperProcessorMock = new Mock<ILocationHelperProcessor>();
            _decoderServiceMock = new Mock<IDecoder>();
            communicationHandler = new CommunicationHandler(_decoderServiceMock.Object, _locationHelperProcessorMock.Object);
        }

        [Fact]
        public void Test_Robot_CommunicationHandler_Constructor_ForDefense_When_Dependency_Null_Result_ThrowException()
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {
                new CommunicationHandler(null,null);
            });

            //Assert
            Assert.Throws<ArgumentNullException>(arrange);
        }

        [Fact]
        public void Test_Robot_CommunicationHandler_ConvertInputToCommandList_When_PositionIsNull_ArgumentNullException()
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {
                communicationHandler.ConvertInputToCommandList("", null);
            });

            //Assert
            Assert.Throws<ArgumentNullException>(arrange);
        }


        private (bool success, List<RobotCommand> robotCommands) GiveMeBackBadTestObject()
        {
            return (false, null);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Test_Robot_CommunicationHandler_ConvertInputToCommandList_When_InputNullOrEmpty_ArgumentNullException( string input)
        {
            //Arrange

            //Act
            var result = communicationHandler.ConvertInputToCommandList(input, new Common.Models.RobotPosition());
         
            //Assert
            Assert.False(result.success);
            Assert.Null(result.robotCommands);
        }
    }
}
