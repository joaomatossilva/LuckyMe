using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LuckyMe.Extensions;
using LuckyMe.Models;

namespace LuckyMe.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserIdAsGuid();
            var drawsPerGame = await (from draws in db.Draws
                where draws.UserId == userId
                group draws by draws.Game.Category
                into drawsGrouppedByCategory 
                select new PerCategoryViewModel<EarningsPerGameViewModel>
                {
                    CatergoryName = drawsGrouppedByCategory.Key.Name,
                    Items = from drawsGroupped in drawsGrouppedByCategory
                            group drawsGroupped by drawsGroupped.Game
                            into drawsGrouppedByGame
                            select new EarningsPerGameViewModel
                            {
                                Game = drawsGrouppedByGame.Key,
                                Count = drawsGrouppedByGame.Count(),
                                CountWithAward = drawsGrouppedByGame.Count(d => d.Award > 0),
                                TotalCost = drawsGrouppedByGame.Sum(d => d.Cost),
                                TotalAward = drawsGrouppedByGame.Sum(d => d.Award)
                            }
                }).ToListAsync();
            return View(drawsPerGame);
        }

        //No async can be used were because we're a child action
        [ChildActionOnly]
        public PartialViewResult SummaryEarningCosts()
        {
            var userId = User.Identity.GetUserIdAsGuid();
            var summaryEarningCosts = (from draws in db.Draws
                where draws.UserId == userId
                group draws by draws.UserId
                into userDraws
                select new SummaryEarningCostsViewModel()
                {
                    TotalCost = userDraws.Sum(d => d.Cost),
                    TotalAward = userDraws.Sum(d => d.Award)
                }).FirstOrDefault();
            return PartialView(summaryEarningCosts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}