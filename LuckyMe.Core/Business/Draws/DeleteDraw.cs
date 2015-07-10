using System;
using MediatR;

namespace LuckyMe.Core.Business.Draws
{
    public class DeleteDraw : IAsyncRequest<Unit>
    {
        public int Id { get; set; }

        [CurrentUserId]
        public Guid UserId { get; set; }
    }
}
