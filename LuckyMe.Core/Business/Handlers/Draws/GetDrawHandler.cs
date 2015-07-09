using System;
using System.Data.Entity;
using System.Threading.Tasks;
using LuckyMe.Core.Business.Draws;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.Draws
{
    public class GetDrawHandler : IAsyncRequestHandler<GetDraw, Draw>
    {
        private readonly ApplicationDbContext _db;

        public GetDrawHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Draw> Handle(GetDraw message)
        {
            return await _db.Draws
                .Include(d => d.Game)
                .SingleOrDefaultAsync(d => d.Id == message.Id && d.UserId == message.UserId);
        }
    }
}
