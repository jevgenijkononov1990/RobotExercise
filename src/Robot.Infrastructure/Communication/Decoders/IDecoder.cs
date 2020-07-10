using Robot.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Infrastructure.Communication.Decoders
{
    public interface IDecoder
    {
        List<DecodedInput> DecodeInput(string textToDecode);

    }
}
