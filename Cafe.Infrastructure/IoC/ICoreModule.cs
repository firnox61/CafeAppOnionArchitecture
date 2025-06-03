using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Infrastructure.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
