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

        // GET: Draws
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserIdAsGuid();
            var draws = db.Draws.Include(d => d.Game).Include(d => d.User).Where(g => g.UserId == userId).OrderByDescending(d => d.Date);
            return View(await draws.ToListAsync());
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
            return View(draw);
        }

        // POST: Draws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Draw draw = await db.Draws.FindAsync(id);
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
