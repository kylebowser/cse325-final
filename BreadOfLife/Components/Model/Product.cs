using System.ComponentModel.DataAnnotations;

namespace BreadOfLife.Components.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string Description { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string Ingredients { get; set; }

        [Required, MinLength(3), MaxLength(200)]
        public string Allergens { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Price { get; set; }

        [Required, RegularExpression(@"^([0-9]{5})$")]
        public string Time { get; set; }
    }
}