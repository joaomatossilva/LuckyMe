using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuckyMe.Extensions;
using LuckyMe.Models;

namespace LuckyMe.Controllers
{
    public class DrawsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const int ItemsPerPage = 10;

        // GET: Draws
        public async Task<ActionResult> Index(int? page )
        {
            page = page ?? 1;
            var userId = User.Identity.GetUserIdAsGuid();
            var total = db.Draws.Count(g => g.UserId == userId);
            var draws = await db.Draws.Include(d => d.Game).Include(d => d.User)
                .Where(g => g.UserId == userId)
                .OrderByDescending(d => d.Date)
                .Skip((int)(page - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToListAsync();

            var paged = new Paged<Draw>
                        {
                            Items = draws,
                            ItemsTotalCount = total,
                            ItemsPerPage = ItemsPerPage,
                            CurrentPage = (int) page
                        };
            return View(paged);
        }


        // GET: Draws/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Draw draw = await db.Draws.Include(d => d.Game).SingleOrDefaultAsync(d => d.Id == id);
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
                Draw drawEntry = await db.Draws.FindAsync(draw.Id);
                if (drawEntry.UserId != User.Identity.GetUserIdAsGuid())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
                }

                drawEntry.Date = draw.Date;
                drawEntry.Cost = draw.Cost;
                drawEntry.Award = draw.Award;

                await db.SaveChangesAsync();
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
            Draw draw = await db.Draws.Include(d => d.Game).SingleOrDefaultAsync(d => d.Id == id);
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
            Draw draw = await db.Draws.FindAsync(id);
            if (draw.UserId != User.Identity.GetUserIdAsGuid())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Draws.Remove(draw);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
