using System.ComponentModel.DataAnnotations;

namespace BreadOfLife.Models
{
    public class CartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CheckoutModel
    {
        [Required(ErrorMessage = "Missionary's full name is required.")]
        public string MissionaryName { get; set; }

        [Required(ErrorMessage = "Assigned mission is required (e.g., Utah Provo Mission).")]
        public string MissionName { get; set; }
    }
}