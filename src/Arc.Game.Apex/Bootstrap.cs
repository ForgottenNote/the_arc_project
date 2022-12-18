using Microsoft.Extensions.DependencyInjection;
using Arc.Game.Apex.Services;

namespace Arc.Game.Apex
{
    public static class Bootstrap
    {
        #region Statics

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Config>();
            services.AddSingleton<DataService>();
            services.AddHostedService(x => x.GetRequiredService<DataService>());
            services.AddHostedService<LinuxService>();
            services.AddHostedService<WindowsService>();
            Feature.Aim.Bootstrap.ConfigureServices(services);
            Feature.Sense.Bootstrap.ConfigureServices(services);
        }

        #endregion
    }
}