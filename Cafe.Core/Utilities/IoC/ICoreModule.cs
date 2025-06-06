using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
