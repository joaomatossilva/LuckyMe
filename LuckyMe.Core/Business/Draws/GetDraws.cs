using System;
using LuckyMe.Core.Data;
using LuckyMe.Core.ViewModels;
using MediatR;

namespace LuckyMe.Core.Business.Draws
{
    public class GetDraws : IAsyncRequest<Paged<Draw>>
    {
        public GetDraws()
        {
            Page = 1;
        }

        public int ItemsPerPage { get; set; }

        [CurrentUserId]
        public Guid UserId { get; set; }
        public int Page { get; set; }
        public int? GameId { get; set; }
        public DateTime? Date { get; set; }

        public Paged<Draw> Results { get; set; } 
    }
}