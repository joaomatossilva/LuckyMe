using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.Core.Business.Draws;
using LuckyMe.Core.Data;
using LuckyMe.Core.ViewModels;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.Draws
{
    public class GetDrawsHandler : IAsyncRequestHandler<GetDraws,Paged<Draw>>
    {
        private readonly ApplicationDbContext _db;

        public GetDrawsHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Paged<Draw>> Handle(GetDraws message)
        {
            var basequery = _db.Draws.Where(d => d.UserId == message.UserId);
            if (message.GameId != null)
            {
                basequery = basequery.Where(d => d.GameId == message.GameId);
            }
            if (message.Date != null)
            {
                basequery = basequery.Where(d => DbFunctions.TruncateTime(d.Date) == message.Date);
            }

            var total = await basequery.CountAsync();
            var draws = await basequery.Include(d => d.Game).Include(d => d.User)
                .OrderByDescending(d => d.Date)
                .Skip((message.Page - 1) * message.ItemsPerPage)
                .Take(message.ItemsPerPage)
                .ToListAsync();

            return new Paged<Draw>
            {
                Items = draws,
                ItemsTotalCount = total,
                ItemsPerPage = message.ItemsPerPage,
                CurrentPage = message.Page
            };
        }
    }
}