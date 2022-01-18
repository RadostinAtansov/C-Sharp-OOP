
namespace MilitaryElite.Models
{

    using MilitaryElite.Enumerations;
    using MilitaryElite.Exceptions;
    using MilitaryElite.Interfaces;
    using System;

    public class Mission : IMission
    {

        public Mission(string codeName, string state)
        {
            this.CodeName = codeName;
            this.State = this.TryParseState(state);
        }

        public string CodeName { get; set; }

        public State State { get; set; }

        public void CompleteMision()
        {
            if (this.State == State.Finished)
            {
                throw new InvalidMissionComplitionException();
            }

            this.State = State.Finished;
        }

        private State TryParseState(string stateStr)
        {
            State state;

            bool parse = Enum.TryParse<State>(stateStr, out state);

            if (!parse)
            {
                throw new InvalidStateException();
            }

            return state;
        }

        public override string ToString()
        {
            return $"Code Name: {this.CodeName} State: {this.State.ToString()}";
        }

    }
}
