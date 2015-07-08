using System.Collections.Generic;

namespace LuckyMe.Core.ViewModels
{
    public class PerCategoryViewModel<T>
    {
        public string CatergoryName { get; set; }
        public IEnumerable<T> Items { get; set; } 
    }
}