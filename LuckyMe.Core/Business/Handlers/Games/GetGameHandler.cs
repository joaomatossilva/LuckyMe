using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using LuckyMe.Core.Business.Games;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.Games
{
    public class GetGameHandler : IAsyncRequestHandler<GetGame,Game>
    {
        private readonly ApplicationDbContext _db;

        public GetGameHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Game> Handle(GetGame message)
        {
            return await _db.Games.Include(g => g.Category).SingleAsync(g => message.GameId == g.Id);
        }
    }
}
