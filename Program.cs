using BlazorServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseOrleans(builder =>
    {
        builder.AddRedisGrainStorage("Redis", optionsBuilder => optionsBuilder.Configure(options =>
        {
            options.ConnectionString = "localhost:6379"; // This is the default
            options.UseJson = true;
            options.DatabaseNumber = 1;
        }));
        builder.UseLocalhostClustering();
        builder.AddMemoryGrainStorageAsDefault();
        builder.AddSimpleMessageStreamProvider("SMS");
        builder.AddMemoryGrainStorage("PubSubStore");
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .RunConsoleAsync();
