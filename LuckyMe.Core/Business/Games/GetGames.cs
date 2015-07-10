using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Games
{
    public class GetGames : IAsyncRequest<IEnumerable<Game>>
    {
    }
}
