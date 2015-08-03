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
    public static class ChangePassword
    {
        public class Command : IAsyncRequest<IdentityResult>
        {
            [CurrentUserId]
            public Guid UserId { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } 
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
                var result = await _userManager.ChangePasswordAsync(message.UserId, message.OldPassword, message.NewPassword);
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
