using DotLiveTeste.Models;
using DotLiveTeste.Repositories.Products;

namespace DotLiveTeste.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IProductRepository _repository;

    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
    public ObservableCollection<Product> ProductsCart { get; set; } = new ObservableCollection<Product>();

    [ObservableProperty]
    int productsInCart;

    public MainViewModel(IProductRepository repository)
    {
        _repository = repository;
        ProductsInCart = 5;
    }

    internal async Task InitAsync()
    {
        var products = await _repository.GetAllAsync();
        Products?.Clear();

        foreach (var product in products)
            Products?.Add(product);
    }

    [RelayCommand]
    public async Task AddCart(Product product)
    {
        ProductsCart?.Add(product);
    }

    [RelayCommand]
    public async Task GoToCartItems()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { "ProductsCart", ProductsCart }
        };
        await Shell.Current.GoToAsync(nameof(CartItemsPage), navigationParameter);
    }
}
