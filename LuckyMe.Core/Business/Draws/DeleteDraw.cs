using System;
using MediatR;

namespace LuckyMe.Core.Business.Draws
{
    public class DeleteDraw : IAsyncRequest<Unit>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }
}
