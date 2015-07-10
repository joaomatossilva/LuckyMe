using System;
using Autofac;
using MediatR;

namespace LuckyMe.Core.Extensions
{
    public static class AutofacBuilderExtensions
    {
        private static string HandlerKey = "handler";
        private static string AsyncHandlerKey = "async-handler";

        public static void RegisterRequestDecorator(this ContainerBuilder builder, string name, Type decoratorType)
        {
            builder.RegisterGenericDecorator(decoratorType, typeof(IRequestHandler<,>),
                    fromKey: HandlerKey).Named(name, typeof(IRequestHandler<,>));

            HandlerKey = name;
        }

        public static void RegisterAsyncRequestDecorator(this ContainerBuilder builder, string name, Type decoratorType)
        {
            builder.RegisterGenericDecorator(decoratorType, typeof(IAsyncRequestHandler<,>),
                fromKey: AsyncHandlerKey).Named(name, typeof(IAsyncRequestHandler<,>));

            AsyncHandlerKey = name;
        }
    }
}
