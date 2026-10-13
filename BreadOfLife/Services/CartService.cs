using BreadOfLife.Models;

namespace BreadOfLife.Services
{
    public class CartService
    {
        public List<CartItem> Items { get; private set; } = new List<CartItem>();
        public event Action? OnCartChanged;

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

        // Allow removing an item completely
        public void RemoveFromCart(string id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Items.Remove(item);
                NotifyStateChanged();
            }
        }

        // Allow increasing or decreasing quantity
        public void UpdateQuantity(string id, int newQuantity)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                if (newQuantity <= 0)
                {
                    Items.Remove(item); // If it drops to 0, remove it
                }
                else
                {
                    item.Quantity = newQuantity;
                }
                NotifyStateChanged();
            }
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