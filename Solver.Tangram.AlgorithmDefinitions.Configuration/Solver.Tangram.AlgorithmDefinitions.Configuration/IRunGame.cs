namespace Solver.Tangram.Game.Logic
{
    public interface IRunGame
    {
        public Task<object> RunGameAsync(CancellationToken ct = default);
    }
}