using MilitaryElite.Core;
using MilitaryElite.Core.Contracts;
using MilitaryElite.IO;
using MilitaryElite.IO.Contracts;

namespace MilitaryElite
{
    class Program
    {
        static void Main(string[] args)
        {

            IReader reader = new ConsoleReader();
            IWriter write = new ConsoleWriter();

            IEngine engine = new Engine(reader, write);
            engine.Run();
        }
    }
}
