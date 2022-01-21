using BlazorServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers;
using Orleans.Serialization;
using System.Reflection;

await Host.CreateDefaultBuilder(args)
    .UseOrleans(builder =>
    {
        builder.AddRedisGrainStorage("Redis", optionsBuilder => optionsBuilder.Configure(options =>
        {
            options.ConnectionString = "localhost:6379"; // This is the default used by Redis docker image
            options.UseJson = true;
            options.DatabaseNumber = 1;
        }));
        builder.ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory());
        builder.UseDashboard(options => { options.CounterUpdateIntervalMs = 10000; }); //at http://localhost:8080
        //this throws, apparently Orleans attempts to use Json for all objects if added.
        //builder.Configure<SerializationProviderOptions>(options => options.SerializationProviders.Add(typeof(OrleansJsonSerializer).GetTypeInfo()));
        builder.UseLocalhostClustering();
        builder.AddMemoryGrainStorageAsDefault();
        builder.AddMemoryStreams<DefaultMemoryMessageBodySerializer>("SMS");
        //builder.AddSimpleMessageStreamProvider("SMS");
        builder.AddMemoryGrainStorage("PubSubStore");
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .RunConsoleAsync();
