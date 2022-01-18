
namespace MilitaryElite.Exceptions
{

    using System;

    public class InvalidCorpsException :Exception
    {

        private const string DEF_EXC_MSG = "Invalid Corps";

        public InvalidCorpsException()
            : base(DEF_EXC_MSG)
        {

        }

        public InvalidCorpsException(string message)
            : base(message)
        {

        }
    }
}
