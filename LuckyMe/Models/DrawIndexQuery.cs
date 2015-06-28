using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyMe.Models
{
    public class DrawIndexQuery
    {
        public DrawIndexQuery()
        {
            Page = 1;
        }

        public int Page { get; set; }
        public int? GameId { get; set; }
        public DateTime? Date { get; set; }

        public Paged<Draw> Results { get; set; } 
    }
}