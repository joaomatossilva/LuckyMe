using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.Data;
using MediatR;

namespace LuckyMe.Core.Business.Draws
{
    public class GetDraw : IAsyncRequest<Draw>
    {
        public int Id { get; set; }

        [CurrentUserId]
        public Guid UserId { get; set; }
    }
}
