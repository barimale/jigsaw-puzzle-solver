namespace Solver.Tangram.Game.Logic
{
    public interface IRunGame
    {
        public Task<T?> RunGameAsync<T>(CancellationToken ct = default)
            where T : class;
    }
}