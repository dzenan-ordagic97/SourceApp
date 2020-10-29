using Prism.Navigation;
using SourceApp.Mobile.ViewModels;
using Xamarin.Forms;

namespace SourceApp.Mobile.Views
{
    public partial class HomePage : ContentPage
    {
        private HomePageViewModel model = null;

        public HomePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}
