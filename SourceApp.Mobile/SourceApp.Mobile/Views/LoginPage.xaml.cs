using Xamarin.Forms;

namespace SourceApp.Mobile.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            CustomEntry.BorderWidth = 0;
            CustomEntry.BorderRadius = 5;
            CustomEntry.BorderColor = Color.White;
            CustomEntry.WidthRequest = 12;
            CustomEntry.HeightRequest = 60;
            CustomEntry.LeftPadding = 10;
            CustomEntry.RightPadding = 10;
            CustomEntry.HorizontalOptions = LayoutOptions.FillAndExpand;
            CustomEntry.VerticalOptions = LayoutOptions.FillAndExpand;
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
