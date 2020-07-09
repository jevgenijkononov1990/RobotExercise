using Robot.Common.Logging;
using System;
using System.Reflection;

namespace Robot.Common.Enms
{
    public enum Direction
    {
        N, 
        E,
        S,
        W,
        NE,
        SE,
        SW,
        NW
    }


    public static class DirectionConverter
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static string DirectionConverterException = "Direction converter error. Error in parsing value";

        public static bool ConvertDirectionEnumToTextValue(Direction directionToConvert, out string enumTxt)
        {
            enumTxt = null;
            if (!Enum.IsDefined(typeof(Direction), directionToConvert))
            {
                _log.Message(LogLevel.Error, ($"{DirectionConverterException}: Value: {directionToConvert}"));
                return false;
            }

            switch (directionToConvert)
            {
                case Direction.N:  enumTxt =  "North";     return true;
                case Direction.E:  enumTxt =  "East";      return true;
                case Direction.S:  enumTxt =  "South";     return true;
                case Direction.W:  enumTxt =  "West";      return true;
                case Direction.NE: enumTxt =  "Northeast"; return true;
                case Direction.SE: enumTxt =  "Southeast"; return true;
                case Direction.SW: enumTxt =  "Southwest"; return true;
                case Direction.NW: enumTxt =  "Northwest"; return true;
                default:
                    _log.Message(LogLevel.Error, ($"{DirectionConverterException}: Value: {directionToConvert}"));
                    return false;
            }
        }

        public static bool ConvertDirectionTextValueToEnum(string strToConvrt, out Direction? direction)
        {
            direction = null;
            if (string.IsNullOrWhiteSpace(strToConvrt))
            {
                _log.Message(LogLevel.Error, ($"{DirectionConverterException}: Value: {strToConvrt}"));
                return false;
            }

            strToConvrt = strToConvrt.ToLower();

            switch (strToConvrt)
            {
                case "north":     direction = Direction.N;  return true;
                case "east":      direction = Direction.E;  return true;
                case "south":     direction = Direction.S;  return true;
                case "west":      direction = Direction.W;  return true;
                case "northeast": direction = Direction.NE; return true;
                case "southeast": direction = Direction.SE; return true;
                case "southwest": direction = Direction.SW; return true;
                case "northwest": direction = Direction.NW; return true;
                default:
                    _log.Message(LogLevel.Error, ($"{DirectionConverterException}: Value: {strToConvrt}"));
                    return false;
            }
        }
    }
}
