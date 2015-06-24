using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LuckyMe.Extensions;
using LuckyMe.Models;

namespace LuckyMe.Controllers
{
    [Authorize]
    public class NewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var gameList = await db.Games.ToListAsync();
            return View(gameList);
        }

        // GET: New
        public async Task<ActionResult> Draw(int id)
        {
            var game = await db.Games.Include(g => g.Category).SingleAsync(g => id == g.Id);
            var draw = new Draw
                       {
                           GameId = id,
                           Cost = game.BasePrice,
                           Date = DateTime.UtcNow
                       };
            FillUpGameViewBags(game);
            return View(draw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Draw([Bind(Include = "GameId,Date,Cost,Award")]Draw draw, int id)
        {
            var game = await db.Games.Include(g => g.Category).SingleAsync(g => id == g.Id);
            if (draw.GameId != id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid game");
            }
            if (ModelState.IsValid)
            {
                draw.UserId = User.Identity.GetUserIdAsGuid();
                db.Draws.Add(draw);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            FillUpGameViewBags(game);
            return View(draw);
        }

        private void FillUpGameViewBags(Game game)
        {
            ViewBag.Category = game.Category.Name;
            ViewBag.Game = game.Name;            
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