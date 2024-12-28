using WordLiveClock.Views;

namespace WordLiveClock
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomePageView();
        }
    }
}
