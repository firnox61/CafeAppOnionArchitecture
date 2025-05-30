﻿using Castle.DynamicProxy;
namespace Cafe.Infrastructure.Aspects.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {// public override void Intercept(IInvocation invocation) bu kısım aslında metotlar mesela
         // add metodu Onbeforenin içini doldurup ilk çalışanların neler yapacağını belirteceiğiz
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
        public override async Task InterceptAsync(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                await invocation.ProceedAsync(); // 🔥 async işlemi burada await et
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                    OnSuccess(invocation);
            }
            OnAfter(invocation);
        }

    }
}
