using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using SourceApp.Mobile.Model;
using SourceApp.Mobile.Services;
using SourceApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SourceApp.Mobile.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly RestService _service = new RestService("posts");
        private readonly RestService _servicePhotos = new RestService("photos");

        public HomePageViewModel(INavigationService navigationService):base(navigationService)
        {
            InitCommand2 = new Command(async () => await Init2());
            PostsCommand = new Command(async () => await Posts());
            Init2();
        }
        public ObservableCollection<Post> PostsList { get; set; } = new ObservableCollection<Post>();
        public ObservableCollection<Photos> PhotosList { get; set; } = new ObservableCollection<Photos>();

        public ICommand InitCommand2 { get; set; }
        public ICommand PostsCommand { get; set; }

        public async Task Posts()
        {
            //await NavigationService.NavigateAsync("/Navigation/WelcomeTabbedPage/PostsPage");
            await NavigationService.SelectTabAsync("PostsPage");
        }
        public async Task Init2()
        {
            //_sort=id&_order=desc&_start=0&_end=5
            string start = "0";
            string end = "5";
            string order = "desc";
            string id = "id";
            var photosList = await _servicePhotos.GetQuery<List<Photos>>(new { _sort = id, _order = order, _start = start, _end = end });
            var postsList = await _service.GetQuery<List<Post>>(new { _sort = id, _order = order, _start = start, _end = end });
            PhotosList.Clear();
            foreach (var photo in photosList)
            {
                PhotosList.Add(photo);
            }
            PostsList.Clear();
            foreach (var post in postsList)
            {
                PostsList.Add(post);
            }
        }

    }
}
