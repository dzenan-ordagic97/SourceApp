using Xamarin.Forms;

namespace SourceApp.Mobile.Views
{
    public partial class WelcomeTabbedPage : TabbedPage
    {
        public WelcomeTabbedPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
