using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace LuckyMe.Core.Extensions
{
    public static class AutofacBuilderExtensions
    {
        public static void RegisterHandlers(this ContainerBuilder builder, Type handlerType, params Type[] decorators)
        {
            RegisterHandlers(builder, handlerType);
            for (var i = 0; i < decorators.Length; i++)
            {
                RegisterGenericDecorator(
                    builder,
                    decorators[i],
                    handlerType,
                    fromKeyType: i == 0 ? handlerType : decorators[i - 1],
                    isTheLast: i == decorators.Length - 1);
            }
        }

        private static void RegisterHandlers(ContainerBuilder builder, Type handlerType)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As(t => t.GetInterfaces()
                        .Where(v => v.IsClosedTypeOf(handlerType))
                        .Select(v => new KeyedService(handlerType.Name, v)))
                .InstancePerRequest();
        }

        private static void RegisterGenericDecorator(ContainerBuilder builder, Type decoratorType, Type decoratedServiceType, Type fromKeyType, bool isTheLast)
        {
            var result = builder.RegisterGenericDecorator(decoratorType, decoratedServiceType, fromKeyType.Name);
            if (!isTheLast)
            {
                result.Keyed(decoratorType.Name, decoratedServiceType);
            }
        }
    }
}
