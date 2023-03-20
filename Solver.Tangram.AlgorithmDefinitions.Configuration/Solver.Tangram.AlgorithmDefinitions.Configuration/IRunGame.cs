namespace Solver.Tangram.Configuration
{
    public interface IRunGame
    {
        public Task<object> RunGameAsync(CancellationToken ct = default);
    }
}