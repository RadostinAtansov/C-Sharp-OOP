namespace MilitaryElite.Exceptions
{

    using System;

    public class InvalidMissionComplitionException : Exception
    {

        private const string DEF_EXC_MSG = "InvalidMission";

        public InvalidMissionComplitionException() 
            : base(DEF_EXC_MSG)
        {
        }

        public InvalidMissionComplitionException(string message) 
            : base(message)
        {
        }
    }
}
