using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LuckyMe.Core.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        public ApplicationUser(string userName)
            : this()
        {
            UserName = userName;
        }

        public virtual ICollection<Draw> Draws { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class CustomUserRole : IdentityUserRole<Guid> { } 
    public class CustomUserClaim : IdentityUserClaim<Guid> { } 
    public class CustomUserLogin : IdentityUserLogin<Guid> { } 

    public class CustomRole : IdentityRole<Guid, CustomUserRole> 
    { 
        public CustomRole() { } 
        public CustomRole(string name) { Name = name; } 
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUserStore<ApplicationUser, Guid>
    { 
        public CustomUserStore(ApplicationDbContext context) 
            : base(context) 
        { 
        } 
    } 

    public class CustomRoleStore : RoleStore<CustomRole, Guid, CustomUserRole> 
    { 
        public CustomRoleStore(ApplicationDbContext context) 
            : base(context) 
        { 
        } 
    } 
}