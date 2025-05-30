using Cafe.Infrastructure.Aspects.Interceptors;
using Cafe.Infrastructure.Caching;
using Cafe.Infrastructure.IoC;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Aspects.Caching
{
    public class CacheRemoveAspect : MethodInterception
    //datada olan güncelleme yeni ekleme vs durumlarında cachein silinmesi gerekir
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
            Console.WriteLine($"❌ Cache silindi: pattern = {_pattern}");
        }
    }
}
