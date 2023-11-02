using Algorithm.ConstraintsPairing.Model;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Algorithm.ConstraintsPairing
{
    public sealed class Engine : CreateObjectiveFunctionStep
    {
        public Engine()
            : this("SCIP")
        {
            // intentionally left blank
        }

        public Engine(string solverType)
            : base(solverType)
        {
            // intentionally left blank
        }

        public async Task<OutputDataSummary> CalculateAsync(InputData input)
        {
            try
            {
                await base.Initialize(input);

                Solver.ResultStatus resultStatus = _solver.Solve();

                return new OutputDataSummary()
                {
                    IsError = resultStatus != Solver.ResultStatus.OPTIMAL && resultStatus != Solver.ResultStatus.FEASIBLE ? true : false,
                    Reason = string.Empty,
                    Data = new OutputData()
                    {
                        Status = resultStatus,
                        Variables = Variables,
                        Input = input,
                        Pairs = ToPairs(resultStatus, Variables, input.GifterAmount)
                    }
                };
            }
            catch (Exception ex)
            {
                return new OutputDataSummary()
                {
                    IsError = true,
                    Reason = ex.Message,
                    Data = new OutputData()
                    {
                        Status = Solver.ResultStatus.NOT_SOLVED,
                        Input = input,
                    }
                };
            }
        }

        private Pair[] ToPairs(Solver.ResultStatus status, Variable[,] variables, int gifterAmount)
        {
            var result = new List<Pair>();

            if (status == Solver.ResultStatus.OPTIMAL || status == Solver.ResultStatus.FEASIBLE)
            {
                for (int i = 0; i < gifterAmount; ++i)
                {
                    for (int j = 0; j < gifterAmount; ++j)
                    {
                        if (variables[i, j].SolutionValue() > 0.5)
                        {
                            result.Add(
                                new Pair()
                                {
                                    FromIndex = i,
                                    ToIndex = j
                                });
                        }
                    }
                }
            }

            return result.ToArray();
        }
    }
}
