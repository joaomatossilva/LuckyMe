using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using LuckyMe.Core.Business;
using LuckyMe.Core.Business.Draws;
using LuckyMe.Core.Data;
using LuckyMe.Extensions;
using MediatR;

namespace LuckyMe.Controllers
{
    public class DrawsController : Controller
    {
        private readonly IMediator _mediator;
        private const int ItemsPerPage = 10;

        public DrawsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Draws
        public async Task<ActionResult> Index(GetDraws query)
        {
            query.UserId = User.Identity.GetUserIdAsGuid();
            query.ItemsPerPage = ItemsPerPage;
            query.Results = await _mediator.SendAsync(query);
            return View(query);
        }


        // GET: Draws/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var draw = await _mediator.SendAsync(new GetDraw{ Id = (int)id, UserId = User.Identity.GetUserIdAsGuid()});
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
                try
                {
                    await _mediator.SendAsync(new EditDraw { Draw = draw, UserId = User.Identity.GetUserIdAsGuid() });
                }
                catch (BusinessException ex)
                {
                    return new HttpStatusCodeResult(ex.StatusCode);
                }
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
            var draw = await _mediator.SendAsync(new GetDraw { Id = (int)id, UserId = User.Identity.GetUserIdAsGuid() });
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
            try
            {
                await _mediator.SendAsync(new DeleteDraw { Id = id, UserId = User.Identity.GetUserIdAsGuid() });
            }
            catch (BusinessException ex)
            {
                return new HttpStatusCodeResult(ex.StatusCode);
            }
            return RedirectToAction("Index");
        }
    }
}
