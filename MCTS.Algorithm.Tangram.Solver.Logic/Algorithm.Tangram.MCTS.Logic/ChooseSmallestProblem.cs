using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitness;
using GeneticSharp;
using TreesearchLib;

namespace Algorithm.Tangram.MCTS.Logic
{
    public class ChooseSmallestProblem : IMutableState<ChooseSmallestProblem, TangramChromosome, Minimize>
    {
        // constraints
        public const int minChoices = 2;
        public const int maxChoices = 10;
        public const int maxDistance = 50;

        // settings
        private int size;
        private Stack<TangramChromosome> choicesMade;

        private readonly BoardShapeBase board;
        private readonly TangramFitness fitnessService;

        public ChooseSmallestProblem(
            int size,
            TangramFitness fitnessService)
        {
            this.size = size;
            this.fitnessService = fitnessService;
            this.choicesMade = new Stack<TangramChromosome>();
        }

        public bool IsTerminal => choicesMade.Count == size;

        public Minimize Bound => new Minimize(
            (int)(fitnessService.Evaluate(this.choicesMade.Peek()) * 100));

        public Minimize? Quality => IsTerminal ? new Minimize((int)(fitnessService.Evaluate(this.choicesMade.Peek()) * 100)) : null;

        public void Apply(TangramChromosome choice)
        {
            choicesMade.Push(choice);
        }

        public object Clone()
        {
            var clone = new ChooseSmallestProblem(
                this.size,
                this.fitnessService);

            clone.choicesMade = new Stack<TangramChromosome>(choicesMade.Reverse());

            return clone;
        }


         // TODO what for is that
        public IEnumerable<TangramChromosome> GetChoices()
        {
            if (choicesMade.Count >= size)
            {
                yield break;
            }

            object current = null;
            if (choicesMade.Count > 0)
            {
                current = choicesMade.Peek();
            }

            var rng = new FastRandomRandomization();
            
            for (int i = 0; i < rng.GetInt(minChoices, maxChoices); i++)
            {
                yield break;
                //yield return rng.Next(current + 1, current + maxDistance);
            }
        }

        public void UndoLast()
        {
            choicesMade.Pop();
        }

        public override string ToString()
        {
            return $"ChooseSmallestProblem [{string.Join(", ", choicesMade.Reverse())}]";
        }
    }
}