using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using LuckyMe.Core.Business.Games;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.Games
{
    public class GetGamesHandler : IAsyncRequestHandler<GetGames,IEnumerable<Game>>
    {
        private readonly ApplicationDbContext _db;

        public GetGamesHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Game>> Handle(GetGames message)
        {
            return await _db.Games.ToListAsync();    
        }
    }
}
