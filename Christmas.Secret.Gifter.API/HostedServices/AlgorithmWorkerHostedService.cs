using Christmas.Secret.Gifter.API.HostedServices.Hub;
using Christmas.Secret.Gifter.API.Services;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Solver.Tangram.Game.Logic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tangram.GameParts.Logic.GameParts;

namespace Christmas.Secret.Gifter.API.HostedServices
{
    public class AlgorithmWorkerHostedService : IHostedService
    {
        private readonly ILocalesGenerator _localesGenerator;
        private readonly ILogger<AlgorithmWorkerHostedService> _logger;
        private readonly PubSub.Hub _hub;
        private readonly Microsoft.AspNetCore.SignalR.Hub _externalHub;
        private readonly IHubContext<LocalesStatusHub, ILocalesStatusHub> _context;
        private readonly IAlgorithmDetailsService _algService;
        private readonly IGamePartsDetailsService _gamePartsService;

        private Game konfiguracjaGry;

        public AlgorithmWorkerHostedService()
        {
            _hub = PubSub.Hub.Default; // from controller via PubSub
        }

        public AlgorithmWorkerHostedService(
            ILogger<AlgorithmWorkerHostedService> logger,
            IServiceProvider serviceProvider,
            IHubContext<LocalesStatusHub, ILocalesStatusHub> context,
            IAlgorithmDetailsService algService,
            IGamePartsDetailsService gamePartsService)
            : this()
        {
            _context = context;
            _algService = algService;
            _gamePartsService = gamePartsService;
            _logger = logger;
            _localesGenerator = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ILocalesGenerator>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Locales Hosted Service running.");

            _hub.Subscribe<GameSettings>(async (item) =>
            {
                await DoWorkAsync(item);
            });

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync(GameSettings item)
        {
            try
            {
                _logger.LogInformation(
                    "Locales creation in progress. ");

                await _context.Clients.All.OnStartAsync(item.Id);

                if (konfiguracjaGry != null)
                {
                    _logger.LogInformation("KonfiguracjaGry already exists.");

                    konfiguracjaGry = null;
                    GC.Collect();
                }

                var selectedGameSetId = item.GamePartsDetailsId;
                var selectedGameSet = _gamePartsService.GetBy(selectedGameSetId);
                var gameParts = DefineGameParts(selectedGameSet);

                int maxDegreeOfParallelism = -1; // 2048 * 2; // -1

                var selectedAlgorithmId = item.AlgorithmDetailsId;
                var selectedAlgorithmDetails = _algService.GetBy(selectedAlgorithmId);
                var algorithm = DefineAlgorithm(
                    gameParts,
                    maxDegreeOfParallelism,
                    selectedAlgorithmDetails);

                if(algorithm == null || gameParts == null)
                {
                    throw new ArgumentException();
                }

                konfiguracjaGry = new GameBuilder()
                    .WithGamePartsConfigurator(gameParts)
                    .WithAlgorithm(algorithm)
                    .Build();

                konfiguracjaGry.Algorithm.QualityCallback += Algorithm_QualityCallback;


                await konfiguracjaGry.RunGameAsync<AlgorithmResult>();

                _logger.LogInformation(
                    "KonfiguracjaGry creation is succesfully finished. ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                await _context.Clients.All.OnFinishAsync(item.Id);
            }
        }

        private static GameSet? DefineGameParts(GamePartsDetails selectedGameSet)
        {
            GameSet gameParts = null;
            switch (selectedGameSet.Code)
            {
                case "CreateBigBoard":
                    gameParts = GameBuilder
                        .AvalaibleGameSets
                        .CreateBigBoard(withAllowedLocations: true);
                    break;
                case "CreatePolishBigBoard":
                    gameParts = GameBuilder
                        .AvalaibleGameSets
                        .CreatePolishBigBoard(withAllowedLocations: true);
                    break;
                case "CreateMediumBoard":
                    gameParts = GameBuilder
                        .AvalaibleGameSets
                        .CreateMediumBoard(withAllowedLocations: true);
                    break;
                case "CreatePolishMediumBoard":
                    gameParts = GameBuilder
                        .AvalaibleGameSets
                        .CreatePolishMediumBoard(withAllowedLocations: true);
                    break;
                case "CreateSimpleBoard":
                    gameParts = GameBuilder
                        .AvalaibleGameSets
                        .CreateSimpleBoard(withAllowedLocations: true);
                    break;
            }

            return gameParts;
        }

        private static IExecutableAlgorithm? DefineAlgorithm(GameSet gameParts, int maxDegreeOfParallelism, AlgorithmDetails selectedAlgorithmDetails)
        {
            IExecutableAlgorithm algorithm = null;
            switch (selectedAlgorithmDetails.Code)
            {
                case "BreadthFirstTreeSearchAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleTSTemplatesAlgorithms
                        .CreateBreadthFirstTreeSearchAlgorithm(
                            gameParts.Board,
                            gameParts.Blocks,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
                case "DepthFirstTreeSearchAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleTSTemplatesAlgorithms
                        .CreateDepthFirstTreeSearchAlgorithm(
                            gameParts.Board,
                            gameParts.Blocks,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
                case "BinaryDepthFirstTreeSearchAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleTSTemplatesAlgorithms
                        .CreateBinaryDepthFirstTreeSearchAlgorithm(
                            gameParts.Board,
                            gameParts.Blocks,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
                case "OneRootParallelDepthFirstTreeSearchAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleTSTemplatesAlgorithms
                        .CreateOneRootParallelDepthFirstTreeSearchAlgorithm(
                            gameParts.Board,
                            gameParts.Blocks,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
                case "BinaryOneRootParallelDepthFirstTreeSearchAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleTSTemplatesAlgorithms
                        .CreateBinaryOneRootParallelDepthFirstTreeSearchAlgorithm(
                            gameParts.Board,
                            gameParts.Blocks,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
                case "PilotTreeSearchAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleTSTemplatesAlgorithms
                        .CreatePilotTreeSearchAlgorithm(
                            gameParts.Board,
                            gameParts.Blocks,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
                case "ExecutableGeneticAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleGATunedAlgorithms
                        .CreateBigBoardSettings(
                            gameParts.Board,
                            gameParts.Blocks,
                            gameParts.AllowedAngles,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
                case "ExecutableVaryRatiosGeneticAlgorithm":
                    algorithm = GameBuilder
                        .AvalaibleGAVaryRatiosTunedAlgorithms
                        .CreateBigBoardSettings(
                            gameParts.Board,
                            gameParts.Blocks,
                            gameParts.AllowedAngles,
                            1000,
                            maxDegreeOfParallelism: maxDegreeOfParallelism);
                    break;
            }

            return algorithm;
        }

        private void Algorithm_QualityCallback(object sender, Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs.SourceEventArgs e)
        {
            AlgorithmResult result = sender as AlgorithmResult;
            var fitness = result.Fitness;
            // TODO publish via singalR to clients
            _context.Clients.All.OnProgressAsync(fitness);
            _context.Clients.All.OnNewResultFoundAsync(result);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Locales Hosted Service is stopping.");

            return Task.CompletedTask;
        }
    }
}
