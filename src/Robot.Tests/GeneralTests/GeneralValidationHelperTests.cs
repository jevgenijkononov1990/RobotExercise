using Robot.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Robot.Tests.GeneralTests
{
    public class GeneralValidationHelperTests
    {
        public GeneralValidationHelperTests()
        {

        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-111111)]
        [InlineData(-20)]
        public void CheckFunctionality_Of_IsIntegerValueNegative_When_InsertValue_Negative_result_True(int value)
        {
            //Act 
            var result = GeneralValidationHelper.IsIntegerValueNegative(value);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(111111)]
        [InlineData(20)]
        public void CheckFunctionality_Of_IsIntegerValueNegative_When_InsertValue_Positive_result_False(int value)
        {
            //Act 
            var result = GeneralValidationHelper.IsIntegerValueNegative(value);

            //Assert
            Assert.False(result);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(111111)]
        [InlineData(20)]
        public void CheckFunctionality_Of_IsWithin_When_InsertValue_NotInRange_False(int value)
        {
            //Act 
            var result = GeneralValidationHelper.IsWithin(value,0, 111111);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(60)]
        [InlineData(90)]
        public void CheckFunctionality_Of_IsWithin_When_InsertValue_InRange_result_False(int value)
        {
            //Act 
            var result = GeneralValidationHelper.IsWithin(value,10,20);

            //Assert
            Assert.False(result);
        }
    }
}
