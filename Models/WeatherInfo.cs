using Newtonsoft.Json;
using Orleans.Concurrency;
using System;

namespace BlazorServer.Models
{
    [Immutable]
    public record class WeatherInfo
    {
        public WeatherInfo(DateTime date, int temperatureC, string summary, int temperatureF) =>
            (Date, TemperatureC, Summary, TemperatureF) = (date, temperatureC, summary, temperatureF);
            
        [JsonConstructor]
        private WeatherInfo() { }

        public DateTime Date { get; init; }
        public int TemperatureC { get; init; }
        public string Summary { get; init; }
        public int TemperatureF { get; init; }
    }
}