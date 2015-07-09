using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuckyMe.Core.Data;
using LuckyMe.Core.ViewModels;
using LuckyMe.Extensions;
using LuckyMe.Models;

namespace LuckyMe.Controllers
{
    public class DrawsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const int ItemsPerPage = 10;

        public DrawsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Draws
        public async Task<ActionResult> Index(DrawIndexQuery query)
        {
            var userId = User.Identity.GetUserIdAsGuid();
            var basequery = _db.Draws.Where(d => d.UserId == userId);
            if (query.GameId != null)
            {
                basequery = basequery.Where(d => d.GameId == query.GameId);
            }
            if (query.Date != null)
            {
                basequery = basequery.Where(d => EntityFunctions.TruncateTime(d.Date) == query.Date);
            }

            var total = await basequery.CountAsync();
            var draws = await basequery.Include(d => d.Game).Include(d => d.User)
                .OrderByDescending(d => d.Date)
                .Skip((query.Page - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToListAsync();

            var paged = new Paged<Draw>
                        {
                            Items = draws,
                            ItemsTotalCount = total,
                            ItemsPerPage = ItemsPerPage,
                            CurrentPage = query.Page
                        };
            query.Results = paged;
            return View(query);
        }


        // GET: Draws/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draw draw = await _db.Draws.Include(d => d.Game).SingleOrDefaultAsync(d => d.Id == id);
            if (draw == null)
            {
                return HttpNotFound();
            }
            return View(draw);
        }

        // POST: Draws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Cost,Award")] Draw draw)
        {
            if (ModelState.IsValid)
            {
                Draw drawEntry = await _db.Draws.FindAsync(draw.Id);
                if (drawEntry.UserId != User.Identity.GetUserIdAsGuid())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
                }

                drawEntry.Date = draw.Date;
                drawEntry.Cost = draw.Cost;
                drawEntry.Award = draw.Award;

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(draw);
        }

        // GET: Draws/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draw draw = await _db.Draws.Include(d => d.Game).SingleOrDefaultAsync(d => d.Id == id);
            if (draw == null)
            {
                return HttpNotFound();
            }
            if (draw.UserId != User.Identity.GetUserIdAsGuid())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(draw);
        }

        // POST: Draws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Draw draw = await _db.Draws.FindAsync(id);
            if (draw.UserId != User.Identity.GetUserIdAsGuid())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _db.Draws.Remove(draw);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
