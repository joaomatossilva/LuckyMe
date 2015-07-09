using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.Core.Business.UserStats;
using LuckyMe.Core.Data;
using LuckyMe.Core.ViewModels;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.UserStats
{
    public class GetUserStatistiscsOverviewHandler : IAsyncRequestHandler<GetUserStatistiscsOverview, IList<PerCategoryViewModel<EarningsPerGameViewModel>>>
    {
        private readonly ApplicationDbContext _db;

        public GetUserStatistiscsOverviewHandler(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IList<PerCategoryViewModel<EarningsPerGameViewModel>>> Handle(GetUserStatistiscsOverview message)
        {
            var userId = message.UserId;
            return await (from draws in _db.Draws
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
        }
    }
}
