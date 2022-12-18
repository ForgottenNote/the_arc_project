using Microsoft.Extensions.Hosting;
using Arc;

await Host.CreateDefaultBuilder(args)
    .UseSystemd()
    .ConfigureLogging(Startup.ConfigureLogging)
    .ConfigureServices(Startup.ConfigureServices)
    .RunConsoleAsync().ConfigureAwait(false);