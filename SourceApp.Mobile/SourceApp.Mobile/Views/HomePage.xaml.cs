using Prism.Navigation;
using SourceApp.Mobile.ViewModels;
using Xamarin.Forms;

namespace SourceApp.Mobile.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new PostsDetailsPage(((SourceApp.Mobile.Model.Post)e.SelectedItem).id));
        }

        
    }
}
