using System.Threading.Tasks;
using MediatR;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Account
{
    public static class LogOff
    {
        public class Command : IAsyncRequest
        {
        }

        internal class CommandHandler : IAsyncRequestHandler<Command, Unit>
        {
            private readonly IAuthenticationManager _authenticationManager;

            public CommandHandler(IAuthenticationManager authenticationManager)
            {
                _authenticationManager = authenticationManager;
            }

            public Task<Unit> Handle(Command message)
            {
                _authenticationManager.SignOut();
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
