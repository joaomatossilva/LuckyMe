using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Account
{
    public static class ResetPassword
    {
        public class Command : IAsyncRequest<IdentityResult>
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        internal class CommandHandler : IAsyncRequestHandler<Command, IdentityResult>
        {
            private readonly ApplicationUserManager _userManager;

            public CommandHandler(ApplicationUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<IdentityResult> Handle(Command message)
            {
                var user = await _userManager.FindByNameAsync(message.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return IdentityResult.Success;
                }
                return await _userManager.ResetPasswordAsync(user.Id, message.Code, message.Password);
            }
        }
    }
}
