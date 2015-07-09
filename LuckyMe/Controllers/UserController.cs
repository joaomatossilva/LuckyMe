using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.Core.Business.UserStats;
using LuckyMe.Extensions;
using MediatR;

namespace LuckyMe.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var drawsPerGame = await _mediator.SendAsync(new GetUserStatistiscsOverview { UserId = User.Identity.GetUserIdAsGuid() });
            return View(drawsPerGame);
        }

        //No async can be used were because we're a child action
        [ChildActionOnly]
        public PartialViewResult SummaryEarningCosts()
        {
            var summaryEarningCosts = _mediator.Send(new GetUserSummaryEarningCosts {UserId = User.Identity.GetUserIdAsGuid()});
            return PartialView(summaryEarningCosts);
        }
    }
}