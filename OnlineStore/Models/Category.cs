using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models {
    public class Category {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        [Display(Name = "Категорія")]
        public string? Title { get; set; }
    }
}
