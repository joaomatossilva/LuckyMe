using System.Threading.Tasks;
using MediatR;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class LogOffHandler : IAsyncRequestHandler<LogOff, Unit>
    {
        private readonly IAuthenticationManager _authenticationManager;

        public LogOffHandler(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        public Task<Unit> Handle(LogOff message)
        {
            _authenticationManager.SignOut();
            return Task.FromResult(Unit.Value);
        }
    }
}
