using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.Core.Business.Draws;
using LuckyMe.Core.Business.Games;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Controllers
{
    [Authorize]
    public class NewController : Controller
    {
        private readonly IMediator _mediator;

        public NewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            var gameList = await _mediator.SendAsync(new GetGames());
            return View(gameList);
        }

        // GET: New
        public async Task<ActionResult> Draw(int id)
        {
            var game = await _mediator.SendAsync(new GetGame {GameId = id});
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
            if (draw.GameId != id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid game");
            }
            if (ModelState.IsValid)
            {
                await _mediator.SendAsync(new NewDraw {Draw = draw});
                return RedirectToAction("Index");
            }

            var game = await _mediator.SendAsync(new GetGame { GameId = id });
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