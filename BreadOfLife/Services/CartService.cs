using BreadOfLife.Models;

namespace BreadOfLife.Services
{
    public class CartService
    {
        public List<CartItem> Items { get; private set; } = new List<CartItem>();
        public event Action OnCartChanged;

        public void AddToCart(CartItem item)
        {
            var existing = Items.FirstOrDefault(i => i.Name == item.Name);
            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
            NotifyStateChanged();
        }

        public decimal GetTotal() => Items.Sum(i => i.Price * i.Quantity);
        
        public void ClearCart()
        {
            Items.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnCartChanged?.Invoke();
    }
}