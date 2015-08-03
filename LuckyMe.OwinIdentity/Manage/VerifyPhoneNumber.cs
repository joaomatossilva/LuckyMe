using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.Business;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Manage
{
    public static class VerifyPhoneNumber
    {
        public class Query : IAsyncRequest<Command>
        {
            [CurrentUserId]
            public Guid UserId { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class Command : IAsyncRequest<IdentityResult>
        {
            [CurrentUserId]
            public Guid UserId { get; set; }


            [Required]
            [Display(Name = "Code")]
            public string Code { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
        }

        internal class QueryHandler : IAsyncRequestHandler<Query, Command>
        {
            private readonly ApplicationUserManager _userManager;

            public QueryHandler(ApplicationUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<Command> Handle(Query message)
            {
                if (message.PhoneNumber == null)
                {
                    return null;
                }
                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(message.UserId, message.PhoneNumber);
                // Send an SMS through the SMS provider to verify the phone number
                if (code == null)
                {
                    return null;
                }
                return new Command { PhoneNumber = message.PhoneNumber };
            }
        }

        internal class CommandHandler : IAsyncRequestHandler<Command, IdentityResult>
        {
            private readonly ApplicationUserManager _userManager;
            private readonly ApplicationSignInManager _signInManager;

            public CommandHandler(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<IdentityResult> Handle(Command message)
            {
                var result = await _userManager.ChangePhoneNumberAsync(message.UserId, message.PhoneNumber, message.Code);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(message.UserId);
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                }
                return result;
            }
        }
    }
}
