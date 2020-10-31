using SourceApp.Mobile.ViewModels;
using Xamarin.Forms;

namespace SourceApp.Mobile.Views
{
    public partial class PostsPage : ContentPage
    {
        private PostsPageViewModel model = null;

        public PostsPage()
        {
            InitializeComponent();
            BindingContext = model = new PostsPageViewModel(null);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
               await model.FilterMyPosts();
               
            }
            else
            {
                await model.Init();
            }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new PostsDetailsPage(((SourceApp.Mobile.Model.Post)e.SelectedItem).id));
        }

       
    }
}
