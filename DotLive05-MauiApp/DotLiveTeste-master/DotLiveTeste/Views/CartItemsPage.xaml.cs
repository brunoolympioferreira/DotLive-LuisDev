namespace DotLiveTeste.Views;

public partial class CartItemsPage : ContentPage
{
	public CartItemsPage(CartItemsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}