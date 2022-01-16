using BlazorServer.Grains;
using BlazorServer.Models;
using Dapr.Actors;
using Dapr.Actors.Client;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class WeatherForecastService
    {
        public async Task<WeatherInfo[]> GetForecastAsync()
        {
            var actorIdTest = new ActorId(33.ToString());
            string actorType = "WeatherActor";
            var proxy = ActorProxy.Create<IWeatherActor>(actorIdTest, actorType);
            var forecast = await proxy.GetForecastAsync();
            return forecast;
        }
    }
}