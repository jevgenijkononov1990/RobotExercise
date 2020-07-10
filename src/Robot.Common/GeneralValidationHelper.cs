using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Common
{
    public static class GeneralValidationHelper
    {
        public static bool IsIntegerValueNegative(int value)
        {
            if (value < 0)
            { return true; }
            else
            { return false; }
        }

        public static bool IsWithin(this int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }
    }
}
