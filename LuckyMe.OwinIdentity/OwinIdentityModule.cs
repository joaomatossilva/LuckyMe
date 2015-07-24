using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LuckyMe.Core.Business;
using LuckyMe.Core.Data;
using LuckyMe.Core.Extensions;
using MediatR;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

namespace LuckyMe.OwinIdentity
{
    public class OwinIdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomUserStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.RegisterHandlers(typeof(OwinIdentityModule).Assembly, typeof(IRequestHandler<,>), typeof(CurrentUserHandlerWrapper<,>));
            builder.RegisterHandlers(typeof(OwinIdentityModule).Assembly, typeof(IAsyncRequestHandler<,>), typeof(CurrentUserAsyncHandlerWrapper<,>));
        }
    }
}
