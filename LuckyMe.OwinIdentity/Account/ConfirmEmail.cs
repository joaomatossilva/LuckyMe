using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Account
{
    public static class ConfirmEmail
    {
        public class Command : IAsyncRequest<IdentityResult>
        {
            [Required]
            public Guid UserId { get; set; }

            [Required]
            public string Code { get; set; }
        }

        public class CommandlHandler : IAsyncRequestHandler<Command, IdentityResult>
        {
            private readonly ApplicationUserManager _userManager;

            public CommandlHandler(ApplicationUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<IdentityResult> Handle(Command message)
            {
                return await _userManager.ConfirmEmailAsync(message.UserId, message.Code);
            }
        }
    }
}
