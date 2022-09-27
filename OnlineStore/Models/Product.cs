using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models {
    public class Product {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        [Display(Name = "Назва товару")]
        public string? Title { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Опис товару")]

        public string? Description { get; set; }

        [Required]
        [DefaultValue(0D)]
        [Display(Name = "Ціна товару")]

        public double? Price { get; set; }

        [Required]
        [Display(Name = "Категорія")]
        public int ID_Category { get; set; }



        [ForeignKey("ID_Category")]
        public Category? Category { get; set; }
    }
}
