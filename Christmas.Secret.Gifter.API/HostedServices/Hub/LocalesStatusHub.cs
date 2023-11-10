using Christmas.Secret.Gifter.Domain;
using Microsoft.AspNetCore.Authorization;
using SignalRSwaggerGen.Attributes;
using System;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.HostedServices.Hub
{
    [AllowAnonymous]
    [SignalRHub]
    public class LocalesStatusHub : Microsoft.AspNetCore.SignalR.Hub<ILocalesStatusHub>
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId + " is connected");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine(Context.ConnectionId + " is disconnected");

            return base.OnDisconnectedAsync(exception);
        }

        public Task OnStartAsync(string id)
        {
            return Clients?.All?.OnStartAsync(id);
        }

        public Task OnFinishAsync(string id)
        {
            return Clients?.All?.OnFinishAsync(id);
        }

        public Task OnProgressAsync(string fitness)
        {
            return Clients?.All?.OnProgressAsync(fitness);
        }

        public Task OnNewResultFoundAsync(string input)
        {
            return Clients?.All.OnNewResultFoundAsync(input);
        }
    }
}
