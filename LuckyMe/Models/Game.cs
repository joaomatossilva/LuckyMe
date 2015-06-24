using System.ComponentModel.DataAnnotations;

namespace LuckyMe.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}