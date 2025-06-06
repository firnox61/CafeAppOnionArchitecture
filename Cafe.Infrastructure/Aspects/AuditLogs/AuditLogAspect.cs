using Cafe.Core.Aspects.Interceptors;
using Cafe.Core.Utilities.IoC;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Aspects.AuditLogs
{
    public class AuditLogAspect : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditLogAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>()!;
        }

        protected override void OnAfter(IInvocation invocation)
        {
            var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "Anonymous";

            var log = new AuditLog
            {
                UserEmail = userEmail,
                Controller = invocation.TargetType.Name,
                Method = invocation.Method.Name,
                Action = $"{invocation.Method.Name} executed",
                Timestamp = DateTime.UtcNow
            };

            using (var scope = ServiceTool.ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                context.AuditLogs.Add(log);
                context.SaveChanges();
            }
        }
    }
}
