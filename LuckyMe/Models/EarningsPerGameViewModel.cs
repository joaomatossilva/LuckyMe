using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyMe.Models
{
    public class EarningsPerGameViewModel
    {
        public Game Game { get; set; }
        public int Count { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalAward { get; set; }
    }
}