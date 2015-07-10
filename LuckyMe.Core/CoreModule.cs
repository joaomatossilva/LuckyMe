using Autofac;
using LuckyMe.Core.Business;
using LuckyMe.Core.Data;
using LuckyMe.Core.Extensions;
using MediatR;

namespace LuckyMe.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();

            //Setup this assembly Request handlers
            builder.RegisterAssemblyTypes(typeof(CoreModule).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CoreModule).Assembly)
                .AsClosedTypesOf(typeof(IAsyncRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterRequestDecorator("current-userid", typeof (CurrentUserHandlerWrapper<,>));
            builder.RegisterAsyncRequestDecorator("current-userid-async", typeof(CurrentUserAsyncHandlerWrapper<,>));
        }
    }
}
