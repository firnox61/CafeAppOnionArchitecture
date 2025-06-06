using System.Diagnostics;
using Cafe.Infrastructure.Caching;
using Cafe.Core.Utilities.IoC;

namespace Cafe.WebAPI.DependencyInjection
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();//MemoryCacheManager deki Imemoryinterfacimiin karşılığı var

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();//senden cahchemanager isterse momorycachemanager ver
            serviceCollection.AddSingleton<Stopwatch>();//performance iççin

        }
    }
}
