

using WordLiveClock.ViewModels;

namespace WordLiveClock.Views;

public partial class HomePageView : ContentPage
{
	public HomePageView()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();
    }
}