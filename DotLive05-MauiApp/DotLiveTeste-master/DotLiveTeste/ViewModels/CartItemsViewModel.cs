using DotLiveTeste.Models;

namespace DotLiveTeste.ViewModels
{
    [QueryProperty(nameof(ProductsCart), "ProductsCart")]
    public partial class CartItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Product> productsCart;
        public ObservableCollection<Product> ProductsCart
        {
            get => productsCart;
            set
            {
                SetProperty(ref productsCart, value);
                UpdateTotal();
            }
        }

        [ObservableProperty]
        decimal total;

        public CartItemsViewModel()
        {

        }

        void UpdateTotal()
        {
            Total = productsCart.Sum(x => x.Price);
        }

        [RelayCommand]
        public async Task RemoveItem(Product product)
        {
            if (product is null)
                return;

            ProductsCart?.Remove(product);
            UpdateTotal();
        }
    }
}
