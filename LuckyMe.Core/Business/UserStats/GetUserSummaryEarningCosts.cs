using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.ViewModels;
using MediatR;

namespace LuckyMe.Core.Business.UserStats
{
    public class GetUserSummaryEarningCosts : IRequest<SummaryEarningCostsViewModel>
    {
        public Guid UserId { get; set; }
    }
}
