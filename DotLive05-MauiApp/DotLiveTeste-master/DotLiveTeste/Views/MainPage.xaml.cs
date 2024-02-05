namespace DotLiveTeste.Views;

public partial class MainPage : ContentPage
{
	private MainViewModel _viewModel => BindingContext as MainViewModel;
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
		await _viewModel?.InitAsync();
        base.OnAppearing();
    }
}
