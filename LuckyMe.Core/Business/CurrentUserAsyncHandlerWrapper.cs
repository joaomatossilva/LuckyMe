using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LuckyMe.Core.Extensions;
using MediatR;

namespace LuckyMe.Core.Business
{
    public class CurrentUserAsyncHandlerWrapper<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse> where TRequest : IAsyncRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _innerHander;

        public CurrentUserAsyncHandlerWrapper(IAsyncRequestHandler<TRequest, TResponse> innerHandler)
        {
            _innerHander = innerHandler;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            var messageType = typeof (TRequest);
            var userProperties = messageType.GetProperties()
                .Where(prop => prop.IsDefined(typeof(CurrentUserIdAttribute), false));
            foreach (var userProperty in userProperties)
            {
                userProperty.SetValue(message, Thread.CurrentPrincipal.Identity.GetUserIdAsGuid());
            }
            return await _innerHander.Handle(message);
        }
    }
}
