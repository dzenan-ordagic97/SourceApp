using SourceApp.Mobile.ViewModels;
using System;
using Xamarin.Forms;

namespace SourceApp.Mobile.Views
{
    public partial class PostsDetailsPage : ContentPage
    {
        private readonly int _id;

        private PostsDetailsPageViewModel model = null;
        public PostsDetailsPage(int id)
        {
            InitializeComponent();
            BindingContext = model = new PostsDetailsPageViewModel(null);
            _id = id;
            model.postId = id;
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.LoadPostDetails(_id);
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = true;
            EnteredName.Focus();
        }

        void  OnOKButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
        }

        void OnCancelButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
        }

        private void btnReply_Clicked(object sender, EventArgs e)
        {
            overlay.IsVisible = true;
        }
    }
}
