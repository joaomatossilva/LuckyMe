using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyMe.Models
{
    public class SummaryEarningCostsViewModel
    {
        public int TotalGames { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalAward { get; set; }

        public decimal Balance
        {
            get { return TotalAward - TotalCost; }
        }
    }
}