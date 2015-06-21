using System;

namespace LuckyMe.Models
{
    public class Draw
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Cost { get; set; }
        public decimal Award { get; set; }
    }
}