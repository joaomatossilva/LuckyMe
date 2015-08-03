using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LuckyMe.Core.Business;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Manage
{
    public static class AddPhoneNumber
    {
        public class Command : IAsyncRequest
        {
            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string Number { get; set; }

            [CurrentUserId]
            public Guid UserId { get; set; }
        }

        internal class CommandHandler : IAsyncRequestHandler<Command, Unit>
        {
            private readonly ApplicationUserManager _userManager;

            public CommandHandler(ApplicationUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command message)
            {
                // Generate the token and send it
                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(message.UserId, message.Number);
                if (_userManager.SmsService != null)
                {
                    var identityMessage = new IdentityMessage
                    {
                        Destination = message.Number,
                        Body = "Your security code is: " + code
                    };
                    await _userManager.SmsService.SendAsync(identityMessage);
                }
                return Unit.Value;
            }
        }
    }
}
