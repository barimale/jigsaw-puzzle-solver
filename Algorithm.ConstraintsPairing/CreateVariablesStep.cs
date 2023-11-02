using Google.OrTools.LinearSolver;
using System;
using System.Threading.Tasks;

namespace Algorithm.ConstraintsPairing
{
    public class CreateVariablesStep : ICreateStep
    {
        protected Solver _solver;

        public CreateVariablesStep()
            : this("SCIP")
        {
            // intentionally left blank
        }

        public Variable[,] Variables { get; private set; }

        public CreateVariablesStep(string solverType)
        {
            _solver = Solver.CreateSolver(solverType);
        }

        public async virtual Task Initialize(InputData input)
        {
            try
            {
                Variables = CreateVariables(input);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Variable[,] CreateVariables(InputData input)
        {
            Variable[,] x = new Variable[input.GifterAmount, input.GifterAmount];
            for (int i = 0; i < input.GifterAmount; ++i)
            {
                for (int j = 0; j < input.GifterAmount; ++j)
                {
                    if (i == j)
                    {
                        x[i, j] = _solver.MakeIntVar(0, 0, $"No assignment to yourself");
                    }
                    else if (input.Costs[i, j] == 100)
                    {
                        x[i, j] = _solver.MakeIntVar(0, 0, $"No assignment when the participant is excluded");
                    }
                    else
                    {
                        x[i, j] = _solver.MakeIntVar(0, 1, $"gifter_{i}_participant_{j}");
                    }
                }
            }

            return x;
        }
    }
}
