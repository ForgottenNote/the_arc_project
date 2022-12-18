using Microsoft.Extensions.DependencyInjection;
using Arc.Game.Apex.Core.Interfaces;

namespace Arc.Game.Apex.Feature.Aim
{
    public static class Bootstrap
    {
        #region Statics

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Config>();
            services.AddTransient<IFeature, Feature>();
        }

        #endregion
    }
}