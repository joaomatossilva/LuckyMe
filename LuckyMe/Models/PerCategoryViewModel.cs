using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckyMe.Models
{
    public class PerCategoryViewModel<T>
    {
        public string CatergoryName { get; set; }
        public IEnumerable<T> Items { get; set; } 
    }
}