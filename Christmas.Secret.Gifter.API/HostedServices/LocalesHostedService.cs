using Christmas.Secret.Gifter.API.HostedServices.Hub;
using Christmas.Secret.Gifter.API.Services;
using Christmas.Secret.Gifter.Domain;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.HostedServices
{
    public class LocalesHostedService : IHostedService
    {
        private readonly ILocalesGenerator _localesGenerator;
        private readonly IImageExtractor _extractor;
        private readonly ILogger<LocalesHostedService> _logger;
        private readonly PubSub.Hub _hub;
        private readonly IHubContext<LocalesStatusHub, ILocalesStatusHub> _broadcastLocalesStatus;

        public LocalesHostedService()
        {
            _hub = PubSub.Hub.Default;
        }

        public LocalesHostedService(
            ILogger<LocalesHostedService> logger,
            IServiceProvider serviceProvider,
            IHubContext<LocalesStatusHub, ILocalesStatusHub> broadcastLocalesStatus)
            : this()
        {
            _broadcastLocalesStatus = broadcastLocalesStatus;
            _logger = logger;
            _extractor = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IImageExtractor>();
            _localesGenerator = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ILocalesGenerator>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Locales Hosted Service running.");

            //WIP check it later
            _hub.Subscribe<GiftEvent>(async (item) =>
            {
                await DoWorkAsync();
            });

            _hub.Subscribe<Participant>(async (item) =>
            {
                await DoWorkAsync();
            });

            _hub.Subscribe<string>(async (item) =>
            {
                await DoWorkAsync();
            });

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync()
        {
            var id = Guid.NewGuid().ToString();

            try
            {
                _logger.LogInformation(
                    "Locales creation in progress. ");

                await _broadcastLocalesStatus.Clients.All.OnStartAsync(id);

                await Task.Delay(3000);

                await Task.WhenAll(
                    Task.Run(async () =>
                    {
                        await _localesGenerator.GenerateAsync();
                    }),
                    Task.Run(async () =>
                    {
                        await _extractor.SaveLocallyAsync();
                    })
                );


                _logger.LogInformation(
                    "Locales creation is finished. ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                await _broadcastLocalesStatus.Clients.All.OnFinishAsync(id);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Locales Hosted Service is stopping.");

            return Task.CompletedTask;
        }
    }
}
