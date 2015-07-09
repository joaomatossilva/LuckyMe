using System.Linq;
using LuckyMe.Core.Business.UserStats;
using LuckyMe.Core.Data;
using LuckyMe.Core.ViewModels;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.UserStats
{
    public class GetUserSummaryEarningCostsHandler : IRequestHandler<GetUserSummaryEarningCosts, SummaryEarningCostsViewModel>
    {
        private readonly ApplicationDbContext _db;

        public GetUserSummaryEarningCostsHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public SummaryEarningCostsViewModel Handle(GetUserSummaryEarningCosts message)
        {
            var userId = message.UserId;
            return (from draws in _db.Draws
                where draws.UserId == userId
                group draws by draws.UserId
                into userDraws
                select new SummaryEarningCostsViewModel()
                {
                    TotalCost = userDraws.Sum(d => d.Cost),
                    TotalAward = userDraws.Sum(d => d.Award),
                    TotalGames = userDraws.Count()
                }).FirstOrDefault();
        }
    }
}
