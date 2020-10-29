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
    }
}
