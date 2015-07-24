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

            builder.RegisterHandlers(typeof(CoreModule).Assembly, typeof(IRequestHandler<,>), typeof(CurrentUserHandlerWrapper<,>));
            builder.RegisterHandlers(typeof(CoreModule).Assembly, typeof(IAsyncRequestHandler<,>), typeof(CurrentUserAsyncHandlerWrapper<,>));
        }
    }
}