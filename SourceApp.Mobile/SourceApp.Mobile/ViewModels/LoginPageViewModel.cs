using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SourceApp.Mobile.Helpers;
using SourceApp.Mobile.Model;
using SourceApp.Mobile.Services;
using SourceApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SourceApp.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly RestService _service = new RestService("users");
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoginCommand = new Command(async () => await Login());

        }
        string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public ICommand LoginCommand { get; set; }
        async Task Login()
        {
            var email = Email;
            var user = await _service.Login<List<User>>( new { email = email} );
            if(user == null || user.Count() == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Login failed");
            }
            else if (user.Count()>0)
            {
                RestService.Session.email = user[0].email;
                RestService.Session.username = user[0].username;
                RestService.Session.id = user[0].id;
                var page = new NavigationPage(new WelcomeTabbedPage());
                Application.Current.MainPage = page;
                //await NavigationService.NavigateAsync("/NavigationPage/WelcomeTabbedPage");
            }
            //else
            //{
            //    //await Application.Current.MainPage.DisplayAlert("Error", "You don't have permission!", "OK");
            //}
        }
    }
}
