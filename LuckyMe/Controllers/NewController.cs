using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LuckyMe.Core.Data;
using LuckyMe.Core.Extensions;
using LuckyMe.Models;

namespace LuckyMe.Controllers
{
    [Authorize]
    public class NewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NewController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<ActionResult> Index()
        {
            var gameList = await _db.Games.ToListAsync();
            return View(gameList);
        }

        // GET: New
        public async Task<ActionResult> Draw(int id)
        {
            var game = await _db.Games.Include(g => g.Category).SingleAsync(g => id == g.Id);
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
            var game = await _db.Games.Include(g => g.Category).SingleAsync(g => id == g.Id);
            if (draw.GameId != id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid game");
            }
            if (ModelState.IsValid)
            {
                draw.UserId = User.Identity.GetUserIdAsGuid();
                _db.Draws.Add(draw);
                await _db.SaveChangesAsync();
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
    }
}