using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SourceApp.Mobile.Model;
using SourceApp.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SourceApp.Mobile.ViewModels
{
    public class PostsPageViewModel : ViewModelBase
    {
        private readonly RestService _service = new RestService("posts");
        int previous, next, last;
        public PostsPageViewModel(INavigationService navigationService): base(navigationService)
        {
            previous = next = last = 1;
            InitCommand = new Command(async () => await Init());
            PreviousCommand = new Command(async () => await Previous());
            NextCommand = new Command(async () => await Next());
            FilterCommand = new Command(async () => await Filter());
            FilterMyPostsCommand = new Command(async () => await FilterMyPosts());
        }
        bool _isNextEnabled = true;
        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { SetProperty(ref _isNextEnabled, value); }
        }

        bool _isFirstEnabled = true;
        public bool IsFirstEnabled
        {
            get { return _isFirstEnabled; }
            set { SetProperty(ref _isFirstEnabled, value); }
        }
        bool _checkBoxEnabled = false;
        public bool CheckBoxEnabled
        {
            get { return _checkBoxEnabled; }
            set { SetProperty(ref _checkBoxEnabled, value); }
        }
        string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { SetProperty(ref _searchString, value); }
        }

        public ObservableCollection<Post> PostsList { get; set; } = new ObservableCollection<Post>();

        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand InitCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand FilterMyPostsCommand { get; set; }



        public async Task Init()
        {
            string order = "desc";
            string id = "id";
            string page = "1";
            SearchString = "";

            next = 1;
            CheckBoxEnabled = false;
            ResponseWithPaging<Post> result = await _service.GetWithHeader<Post>(new { _sort = id, _order = order, _page=page }, next);
            previous = result.Previous;
            next = result.Next;
            last = result.Last;
            IsFirstEnabled = !result.IsFirst;
            if(!IsNextEnabled)
            {
                IsNextEnabled = true;
            }

            PostsList.Clear();
            foreach (var post in result.Data)
            {
                PostsList.Add(post);
            }
        }
        public async Task Previous()
        {
            string order = "desc";
            string id = "id";

            ResponseWithPaging<Post> result = await _service.GetWithHeader<Post>(new { _sort = id, _order = order, _page = previous }, previous);
            previous = result.Previous;
            next = result.Next;
            last = result.Last;
            IsNextEnabled = !result.IsLast;
            IsFirstEnabled = !result.IsFirst;

            PostsList.Clear();
            foreach (var post in result.Data)
            {
                PostsList.Add(post);
            }
        }
        public async Task Next()
        {
            string order = "desc";
            string id = "id";

            ResponseWithPaging<Post> result = await _service.GetWithHeader<Post>(new { _sort = id, _order = order, _page = next }, next);
            previous = result.Previous;
            next = result.Next;
            last = result.Last;
            IsNextEnabled = !result.IsLast;
            IsFirstEnabled = !result.IsFirst;
            PostsList.Clear();
            foreach (var post in result.Data)
            {
                PostsList.Add(post);
            }
        }
        public async Task FilterMyPosts()
        {
            string page = "1";
            string _q = SearchString;
            next = 1;
            CheckBoxEnabled = true;
            string id = RestService.Session.id.ToString();
            ResponseWithPaging<Post> result = await _service.GetWithHeader<Post>(new { userId = id, _page = page, q = _q  }, next);
            previous = result.Previous;
            next = result.Next;
            last = result.Last;
            IsNextEnabled = !result.IsLast;
            IsFirstEnabled = !result.IsFirst;
            PostsList.Clear();
            foreach (var post in result.Data)
            {
                PostsList.Add(post);
            }


        }
        public async Task Filter()
        {
            string _q = SearchString;
            next = 1;
            string page = "1";
            string id = RestService.Session.id.ToString();
            if(CheckBoxEnabled == true)
            {
                ResponseWithPaging<Post> result = await _service.GetWithHeader<Post>(new { userId=id, _page = page, q = _q }, next);
                previous = result.Previous;
                next = result.Next;
                last = result.Last;
                IsNextEnabled = !result.IsLast;
                IsFirstEnabled = !result.IsFirst;
                PostsList.Clear();
                foreach (var post in result.Data)
                {
                    PostsList.Add(post);
                }
            }
            else
            {
                ResponseWithPaging<Post> result = await _service.GetWithHeader<Post>(new { _page = page, q = _q }, next);
                previous = result.Previous;
                next = result.Next;
                last = result.Last;
                IsNextEnabled = !result.IsLast;
                IsFirstEnabled = !result.IsFirst;
                PostsList.Clear();
                foreach (var post in result.Data)
                {
                    PostsList.Add(post);
                }
            }
        }

    }
}
