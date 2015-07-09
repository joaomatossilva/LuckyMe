using LuckyMe.Core.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LuckyMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var sorteio = context.Categories.FirstOrDefault(c => c.Name == "Sorteio");
            var raspadinha = context.Categories.FirstOrDefault(c => c.Name == "Raspadinha");

            if (sorteio == null)
            {
                sorteio = new Category { Name = "Sorteio" };
                context.Categories.AddOrUpdate(p => p.Name, sorteio);
            }

            if (raspadinha == null)
            {
                raspadinha = new Category { Name = "Raspadinha" };
                context.Categories.AddOrUpdate(p => p.Name, raspadinha);
            }

            context.SaveChanges();

            SeedGames(context,
                new Game { Name = "Euromilhões", IsActive = true, BasePrice = 2, Category = sorteio },
                new Game { Name = "Totoloto", IsActive = true, BasePrice = .9m, Category = sorteio },
                new Game { Name = "Totobola", IsActive = true, BasePrice = .4m, Category = sorteio },
                new Game { Name = "Super Pé-de-Meia", IsActive = true, BasePrice = 5, ImageUrl = "https://www.jogossantacasa.pt/Game/images/raspadinha/Imagem_286.png", Category = raspadinha },
                new Game { Name = "Pé-de-Meia", IsActive = true, BasePrice = 3, Category = raspadinha },
                new Game { Name = "Mini Pé-de-Meia", IsActive = true, BasePrice = 1, Category = raspadinha },
                new Game { Name = "Férias", IsActive = true, BasePrice = 1, Category = raspadinha },
                new Game { Name = "Grande Sorte", IsActive = true, BasePrice = 2, ImageUrl = "https://www.jogossantacasa.pt/Game/images/raspadinha/Imagem_341.png", Category = raspadinha },
                new Game { Name = "Zodíaco da Sorte", IsActive = true, BasePrice = 2, ImageUrl = "https://www.jogossantacasa.pt/Content/images/uploadedImages/content/pjmc/gc/cont/11123/Raspadinha237.jpg", Category = raspadinha },
                new Game { Name = "20x", IsActive = true, BasePrice = 2, ImageUrl = "https://www.jogossantacasa.pt/Content/images/uploadedImages/content/pjmc/gc/cont/8388/Raspadinha216.jpg", Category = raspadinha },
                new Game { Name = "50x", IsActive = true, BasePrice = 5, ImageUrl = "https://www.jogossantacasa.pt/Content/images/uploadedImages/content/pjmc/gc/cont/11386/Raspadinha234.jpg", Category = raspadinha },
                new Game { Name = "100x", IsActive = true, BasePrice = 10, ImageUrl = "https://www.jogossantacasa.pt/Content/images/uploadedImages/content/pjmc/gc/cont/12369/Raspadinha242.jpg", Category = raspadinha },
                new Game { Name = "Trevo da Sorte", IsActive = true, BasePrice = 1, ImageUrl = "https://www.jogossantacasa.pt/Content/images/uploadedImages/content/pjmc/gc/cont/7209/Raspadinha207.jpg", Category = raspadinha },
                new Game { Name = "Procura-se", IsActive = true, BasePrice = 1, ImageUrl = "https://www.jogossantacasa.pt/Content/images/uploadedImages/content/pjmc/gc/cont/7698/Raspadinha226.jpg", Category = raspadinha },
                new Game { Name = "Dias da Semana", IsActive = true, BasePrice = 1, ImageUrl = "", Category = raspadinha },
                new Game { Name = "Portas da Sorte", IsActive = true, BasePrice = 1, ImageUrl = "", Category = raspadinha },
                new Game { Name = "Ano da Cabra", IsActive = true, BasePrice = 1, ImageUrl = "", Category = raspadinha },
                new Game { Name = "Aranha da Sorte", IsActive = true, BasePrice = 1, ImageUrl = "", Category = raspadinha },
                new Game { Name = "Música", IsActive = true, BasePrice = 2, ImageUrl = "", Category = raspadinha },
                new Game { Name = "Fogosos", IsActive = true, BasePrice = 2, ImageUrl = "", Category = raspadinha },
                new Game { Name = "Recompensa", IsActive = true, BasePrice = 3, ImageUrl = "", Category = raspadinha },
                new Game { Name = "20 Anos", IsActive = true, BasePrice = 3, ImageUrl = "", Category = raspadinha },
                new Game { Name = "Mega Pé-de-Meia", IsActive = true, BasePrice = 10, ImageUrl = "", Category = raspadinha }
                );

            context.SaveChanges();

            if (!(context.Roles.Any(u => u.Name == "Admin")))
            {
                var roleStore = new RoleStore<CustomRole, Guid, CustomUserRole>(context);
                var roleManager = new RoleManager<CustomRole, Guid>(roleStore);
                roleManager.Create(new CustomRole("Admin"));
            }

            if (!(context.Users.Any(u => u.UserName == "joao@kspace.pt")))
            {
                var userStore = new CustomUserStore(context);
                var userManager = new ApplicationUserManager(userStore, null);
                var userToInsert = new ApplicationUser { UserName = "joao@kspace.pt", Email = "joao@kspace.pt" };
                var result = userManager.CreateAsync(userToInsert, "Password@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRole(userToInsert.Id, "Admin");
                }
            }
        }

        private void SeedGames(ApplicationDbContext context, params Game[] games)
        {
            foreach (var gameToSeed in games)
            {
                if (!context.Games.Any(g => g.Name == gameToSeed.Name))
                {
                    context.Games.Add(gameToSeed);
                }
            }
        }
    }
}
