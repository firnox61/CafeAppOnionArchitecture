﻿using Cafe.Application.Utilities;
using Cafe.Infrastructure.Aspects.Interceptors;
using Castle.DynamicProxy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Aspects.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception("Bu bir doğrulama sınıfı değildir");
            }

            _validatorType = validatorType;
        }

        public override async Task InterceptAsync(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            var entities = invocation.Arguments
                .Where(t => entityType.IsAssignableFrom(t.GetType()))
                .ToList();

            foreach (var entity in entities)
            {
                await ValidationTool.ValidateAsync(validator, entity); // 🔥 async validation
            }

            await invocation.ProceedAsync(); // metodu çağır
        }
    }

}
