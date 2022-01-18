
namespace MilitaryElite.Exceptions
{

    using System;

    public class InvalidStateException : Exception
    {

        private const string DEF_EXC_MSG = "Invalid State";

        public InvalidStateException() 
            : base(DEF_EXC_MSG)
        {

        }

        public InvalidStateException(string message) 
            : base(message)
        {

        }
    }
}
