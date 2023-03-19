namespace Genetic.Algorithm.Tangram.Configurator
{
    public interface IRunGame
    {
        public Task<object> RunGameAsync(CancellationToken ct = default(CancellationToken));
    }
}