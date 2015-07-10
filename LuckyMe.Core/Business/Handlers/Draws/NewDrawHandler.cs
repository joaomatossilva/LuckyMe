using System;
using System.Threading.Tasks;
using LuckyMe.Core.Business.Draws;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.Draws
{
    public class NewDrawHandler : IAsyncRequestHandler<NewDraw, Unit>
    {
        private readonly ApplicationDbContext _db;

        public NewDrawHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(NewDraw message)
        {
            message.Draw.UserId = message.UserId;
            _db.Draws.Add(message.Draw);
            await _db.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
