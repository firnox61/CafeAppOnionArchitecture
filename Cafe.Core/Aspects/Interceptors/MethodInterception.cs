using Castle.DynamicProxy;
namespace Cafe.Core.Aspects.Interceptors
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
            var isAsync = typeof(Task).IsAssignableFrom(invocation.Method.ReturnType);

            var isSuccess = true;
            OnBefore(invocation);

            try
            {
                if (isAsync)
                {
                    invocation.ReturnValue = InterceptAsyncInternal(invocation);
                }
                else
                {
                    invocation.Proceed();
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }

            if (isSuccess && !isAsync)
            {
                OnSuccess(invocation);
                OnAfter(invocation);
            }
        }

        private async Task InterceptAsyncInternal(IInvocation invocation)
        {
            try
            {
                await invocation.ProceedAsync();
                OnSuccess(invocation);
            }
            catch (Exception e)
            {
                OnException(invocation, e);
                throw;
            }
            finally
            {
                OnAfter(invocation);
            }
        }
    }

    }

