using Robot.Common;
using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Common.Models;
using Robot.Infrastructure.Communication.Decoders;
using Robot.Infrastructure.StateMachine.LocationHelpers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Robot.Infrastructure.Communication
{
    public class CommunicationHandler : ICommunicationHandler
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ILocationHelperProcessor _locationHelperProcessor;
        private readonly IDecoder _decoderService;

        public CommunicationHandler(IDecoder decoderService, ILocationHelperProcessor locationHelperProcessor)
        {
            _decoderService = decoderService 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(decoderService)}");

            _locationHelperProcessor = locationHelperProcessor 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationHelperProcessor)}");
        }

        public (bool success, List<RobotCommand> robotCommands) ConvertInputToCommandList(string txtToCmd, RobotPosition currentPosition)
        {
            if (currentPosition == null)
            {
                _log.Message(LogLevel.Error, "ConvertInputToCommandList error");
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(txtToCmd))
            {
                return (false, null);
            }

            List<DecodedInput> decodedCommands = _decoderService.DecodeInput(txtToCmd.ToUpper());

            if (decodedCommands == null || decodedCommands.Count == 0)
            {
                return (false, null);
            }

            var xToUpdate = currentPosition.X;
            var yToUpdate = currentPosition.Y;
            var directionToUpdate = currentPosition.Direction;

            foreach (DecodedInput item in decodedCommands)
            {
                directionToUpdate = _locationHelperProcessor.ProcessNewDirection(item.RotationType, directionToUpdate);
                var position = _locationHelperProcessor.ProcessNewPosition(directionToUpdate, xToUpdate, yToUpdate, item.MovementDistance);
                xToUpdate = position.X;
                yToUpdate = position.Y;
                //it is possible to store all commands 
                //and create List in a loop
                //But because it is not neccessery now...
                //I will output only one robotCommand after loop

                ///new RobotCommand
            }

            List<RobotCommand> robotCommands = new List<RobotCommand>
            {
                new RobotCommand
                {
                    CurentDirection = directionToUpdate,
                    MoveTo = new Position(xToUpdate, yToUpdate),
                    OriginalCmd = txtToCmd,
                    QueueId = 1,
                    Type = RobotCommandType.Move
                },
            };
            return (true, robotCommands);
        }
    }
}
