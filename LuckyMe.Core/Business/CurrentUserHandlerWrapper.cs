using System.Linq;
using System.Threading;
using LuckyMe.Core.Extensions;
using MediatR;

namespace LuckyMe.Core.Business
{
    public class CurrentUserHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHander;

        public CurrentUserHandlerWrapper(IRequestHandler<TRequest, TResponse> innerHandler)
        {
            _innerHander = innerHandler;
        }

        public TResponse Handle(TRequest message)
        {
            var messageType = typeof (TRequest);
            var userProperties = messageType.GetProperties()
                .Where(prop => prop.IsDefined(typeof(CurrentUserIdAttribute), false));
            foreach (var userProperty in userProperties)
            {
                userProperty.SetValue(message, Thread.CurrentPrincipal.Identity.GetUserIdAsGuid());
            }
            return _innerHander.Handle(message);
        }
    }
}
