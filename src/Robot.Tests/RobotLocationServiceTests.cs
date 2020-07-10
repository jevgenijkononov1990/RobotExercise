using Robot.Common.Enms;
using Robot.Infrastructure.StateMachine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace Robot.Tests
{
    public class RobotLocationServiceTests
    {
        public RobotLocationService robotService;

        public RobotLocationServiceTests()
        {
            robotService = new RobotLocationService();
        }

        [Fact]
        public void RobotLocationServiceTest_When_SetupWorkingMatrix__InputMatrixSizeNUll_ResultFalse()
        {
            //arrange

            //act
            var response = robotService.SetupWorkingMatrix(null);

            //assert
            Assert.False(response);
        }

        [Fact]
        public void RobotLocationServiceTest_When_SetupWorkingMatrix__InputMatrixNotNUll_ResultTrue()
        {
            //arrange

            //act
            var response = robotService.SetupWorkingMatrix(new Common.Models.MatrixSize());

            //assert
            Assert.True(response);
        }


        [Fact]
        public void RobotLocationServiceTest_SetPosition_DirectionCastWrong()
        {
            //arrange
            Action arrange;
            Direction direction = (Direction)(100000);

            //Act
            arrange = new Action(() =>
            {
                robotService.SetPosition(0, 0, direction);
            });

            //Arrange
            Assert.Throws<InvalidEnumArgumentException>(arrange);
        }

        [Theory]
        [InlineData(-1, 20)]
        [InlineData(20, -20)]
        [InlineData(-20, -20)]
        public void RobotLocationServiceTest_SetPosition_When_AnyOf_X_YValuesNegative_OptionsNullOrValueNull_ResultFalse(int x, int y)
        {
            //Arrange

            //act
            var response = robotService.SetPosition(x,y, Direction.E);

            //assert
            Assert.False(response);
        }


        [Theory]
        [InlineData(4, 2)]
        [InlineData(10,10)]
        public void RobotLocationServiceTest_SetPosition_When_RobotNewPosition_IsOutOfrane_ResultFalse(int matrixX, int matrixY)
        {
            //Arrange

            robotService.SetupWorkingMatrix(new Common.Models.MatrixSize(matrixX, matrixY));
            //act
            var response = robotService.SetPosition(100, 100, Direction.E);

            //assert
            Assert.False(response);
        }


        //to finish
    }
}
