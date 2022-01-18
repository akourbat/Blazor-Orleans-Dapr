using System;
using Dapr.Actors.Runtime;
using System.Threading.Tasks;
using BlazorServer.Models;
using Dapr.Actors;
using System.Collections.Immutable;
using Orleans.Concurrency;

namespace BlazorServer.Grains
{
    public interface IWeatherActor : IActor
    {
        Task<WeatherInfo[]> GetForecastAsync();
    }
    public class WeatherActor : Actor, IWeatherActor
    {
        private readonly WeatherInfo[] data = {

            new WeatherInfo(DateTime.Today.AddDays(1), 1, "Freezing", 33),
            new WeatherInfo(DateTime.Today.AddDays(2), 14, "Bracing", 57),
            new WeatherInfo(DateTime.Today.AddDays(3), -13, "Freezing", 9),
            new WeatherInfo(DateTime.Today.AddDays(4), -16, "Balmy", 4),
            new WeatherInfo(DateTime.Today.AddDays(5), -2, "Chilly", 29) };
        
        public WeatherActor(ActorHost host) : base(host) { }

        public Task<WeatherInfo[]> GetForecastAsync() => Task.FromResult(data);
    }
}
