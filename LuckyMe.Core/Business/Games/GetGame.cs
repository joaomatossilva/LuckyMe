using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Games
{
    public class GetGame : IAsyncRequest<Game>
    {
        public int GameId { get; set; }
    }
}
