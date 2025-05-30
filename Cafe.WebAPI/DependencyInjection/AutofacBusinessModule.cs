using Autofac;
using Autofac.Extras.DynamicProxy;
using Cafe.Application.Interfaces.Security;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Application.Services.Managers;
using Cafe.Domain.Security;
using Cafe.Infrastructure.Aspects.Interceptors;
using Cafe.Infrastructure.Persistence.Repositories.EntityFramework;
using Cafe.Infrastructure.Security.Hashing;
using Cafe.Infrastructure.Security.Jwt;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.WebAPI.DependencyInjection
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IngredientManager>().As<IIngredientService>().InstancePerLifetimeScope();
            builder.RegisterType<EfIngredientDal>().As<IIngredientDal>().InstancePerLifetimeScope();


            // builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            builder.RegisterType<ProductManager>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<EfProductDal>().As<IProductDal>().InstancePerLifetimeScope();
            builder.RegisterType<FileServiceManager>().As<IFileService>().InstancePerLifetimeScope();

            builder.RegisterType<PaymentManager>().As<IPaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<EfPaymentDal>().As<IPaymentDal>().InstancePerLifetimeScope();


            builder.RegisterType<DailySalesSummaryManager>().As<IDailySalesSummaryService>().InstancePerLifetimeScope();
            builder.RegisterType<EfDailySalesSummaryDal>().As<IDailySalesSummaryDal>().InstancePerLifetimeScope();

            builder.RegisterType<OrderManager>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().InstancePerLifetimeScope();

            
            builder.RegisterType<TableManager>().As<ITableService>().InstancePerLifetimeScope();
            builder.RegisterType<EfTableDal>().As<ITableDal>().InstancePerLifetimeScope();

            builder.RegisterType<OrderItemManager>().As<IOrderItemService>().InstancePerLifetimeScope();
            builder.RegisterType<EfOrderItemDal>().As<IOrderItemDal>().InstancePerLifetimeScope();

            builder.RegisterType<ProductIngredientManager>().As<IProductIngredientService>().InstancePerLifetimeScope();
            builder.RegisterType<EfProductIngredientDal>().As<IProductIngredientDal>().InstancePerLifetimeScope();

            builder.RegisterType<EfProductionHistoryDal>().As<IProductionHistoryDal>().InstancePerLifetimeScope();

            // builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            // builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            //  builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<HashingService>().As<IHashingService>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();





        }
    }
}
