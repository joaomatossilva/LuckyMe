using System.Net;
using System.Threading.Tasks;
using LuckyMe.Core.Business.Draws;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.Draws
{
    public class EditDrawHandler : IAsyncRequestHandler<EditDraw, Unit>
    {
        private readonly ApplicationDbContext _db;

        public EditDrawHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(EditDraw message)
        {
            var drawEntry = await _db.Draws.FindAsync(message.Draw.Id);
            if (drawEntry.UserId != message.UserId)
            {
                throw new BusinessException(HttpStatusCode.BadRequest);
            }

            drawEntry.Date = message.Draw.Date;
            drawEntry.Cost = message.Draw.Cost;
            drawEntry.Award = message.Draw.Award;

            await _db.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
