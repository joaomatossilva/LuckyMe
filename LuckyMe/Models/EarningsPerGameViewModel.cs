using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuckyMe.Models
{
    public class EarningsPerGameViewModel
    {
        public Game Game { get; set; }
        public int Count { get; set; }
        public int CountWithAward { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalAward { get; set; }

        [DisplayFormat(DataFormatString = "{0:p}")]
        public double AwardRate
        {
            get
            {
                if (Count == 0)
                {
                    return 0;
                }
                return CountWithAward / (double)Count;
            }
        }
    }
}