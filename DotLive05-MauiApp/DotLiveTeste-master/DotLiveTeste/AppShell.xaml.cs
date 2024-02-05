namespace DotLiveTeste;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CartItemsPage), typeof(CartItemsPage));
    }
}
