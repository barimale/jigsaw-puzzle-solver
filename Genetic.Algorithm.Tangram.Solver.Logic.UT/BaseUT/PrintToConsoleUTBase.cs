using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Base
{
    public abstract class PrintToConsoleUTBase
    {
        private readonly ITestOutputHelper Output;

        public PrintToConsoleUTBase(ITestOutputHelper output)
        {
            this.Output = output;
        }

        public void Display(string line)
        {
            this.Output.WriteLine(line);
        }

        public void Display(string[] lines)
        {
            foreach(var line in lines)
            {
                this.Display(line);
            }
        }
    }
}
