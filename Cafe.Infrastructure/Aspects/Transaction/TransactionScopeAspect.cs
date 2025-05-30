﻿using Cafe.Infrastructure.Aspects.Interceptors;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Cafe.Infrastructure.Aspects.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override async Task InterceptAsync(IInvocation invocation)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeAsyncFlowOption.Enabled)) // 🔥 async desteği açık
            {
                try
                {
                    await invocation.ProceedAsync(); // ✅ metodu async şekilde çalıştır
                    transactionScope.Complete(); // başarılıysa commit
                }
                catch (Exception)
                {
                    // rollback otomatik, dispose yeterli
                    throw;
                }
            }//[TransactionScopeAspect] ekleencek
        }
    }
}