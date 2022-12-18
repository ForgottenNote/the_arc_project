using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Arc.Driver.Linux;
using Arc.Game.Apex;
using Arc.Logging;

namespace Arc
{
    public static class Startup
    {
        #region Statics

        public static void ConfigureLogging(ILoggingBuilder builder)
        {
            builder.ClearProviders();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Linux>();
            Bootstrap.ConfigureServices(services);
        }

        #endregion
    }
}