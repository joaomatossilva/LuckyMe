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
    public class ExternalLoginConfirmation : IAsyncRequest<IdentityResult>
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
