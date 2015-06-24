using System;
using System.ComponentModel.DataAnnotations;

namespace LuckyMe.Models
{
    public class Draw
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTimeOffset Date { get; set; }

        [Display(Name = "Custo")]
        public decimal Cost { get; set; }

        [Display(Name = "Prémio")]
        public decimal Award { get; set; }
    }
}