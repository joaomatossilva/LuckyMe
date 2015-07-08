using System;
using System.Collections.Generic;

namespace LuckyMe.Core.ViewModels
{
    public class Paged<T> : IPaged
    {
        public Paged()
        {
            ItemsPerPage = 10;
        } 

        private const int MaxShownPages = 7;

        public IEnumerable<T> Items { get; set; }

        public int ItemsTotalCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int) Math.Ceiling(ItemsTotalCount/(decimal) ItemsPerPage); }
        }

        public int StartPage
        {
            get
            {
                int minStartPage = 1;
                var totalPages = TotalPages;
                if (totalPages <= MaxShownPages)
                {
                    return minStartPage;
                }
                minStartPage = CurrentPage - MaxShownPages/2;
                return minStartPage <= 0 ? 1 : minStartPage;
            }
        }

        public int EndPage
        {
            get
            {
                var maxEndPge = StartPage + MaxShownPages;
                return maxEndPge > TotalPages ? TotalPages : maxEndPge - 1;
            }
        }
    }

    public interface IPaged
    {
        int CurrentPage { get; }
        int StartPage { get; }
        int EndPage { get; }
    }
}