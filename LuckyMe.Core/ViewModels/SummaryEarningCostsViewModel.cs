namespace LuckyMe.Core.ViewModels
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