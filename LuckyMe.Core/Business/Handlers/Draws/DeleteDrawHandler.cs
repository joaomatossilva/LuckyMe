using System.Net;
using System.Threading.Tasks;
using LuckyMe.Core.Business.Draws;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Handlers.Draws
{
    public class DeleteDrawHandler : IAsyncRequestHandler<DeleteDraw, Unit>
    {
        private readonly ApplicationDbContext _db;

        public DeleteDrawHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeleteDraw message)
        {
            Draw draw = await _db.Draws.FindAsync(message.Id);
            if (draw.UserId != message.UserId)
            {
                throw new BusinessException(HttpStatusCode.BadRequest);
            }
            _db.Draws.Remove(draw);
            await _db.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
