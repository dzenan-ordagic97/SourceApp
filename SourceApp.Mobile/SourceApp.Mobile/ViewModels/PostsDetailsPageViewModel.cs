using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SourceApp.Mobile.Model;
using SourceApp.Mobile.Requests;
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
    public class PostsDetailsPageViewModel : ViewModelBase
    {
        private readonly RestService _service = new RestService("posts");
        private readonly RestService _serviceUsers = new RestService("users");
        private readonly RestService _serviceComments = new RestService("comments");
        public int postId;

        public PostsDetailsPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            LoadCommand = new Command(async (param) => await LoadPostDetails((int)param));
            SaveCommand = new Command(async () => await SaveData());
            EditCommand = new Command(async () => await OnEdit());
            CommentCommand = new Command(async () => await  OnReply());
            CommentEditCommand = new Command(async (id) => await EditComment((int)id));
            DeleteCommand = new Command(async () => await DeleteData());

        }



        string _titlePost = string.Empty;
        public string TitlePost
        {
            get { return _titlePost; }
            set { SetProperty(ref _titlePost, value); }
        }
        string _user = string.Empty;
        public string User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        string _bodyPost = string.Empty;
        public string BodyPost
        {
            get { return _bodyPost; }
            set { SetProperty(ref _bodyPost, value); }
        }
        string _commentPost = string.Empty;
        public string CommentPost
        {
            get { return _commentPost; }
            set { SetProperty(ref _commentPost, value); }
        }
        string _date = string.Empty;
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
        int _gridId = 2;
        public int GridID
        {
            get { return _gridId; }
            set { SetProperty(ref _gridId, value); }
        }
        bool _visibleEdit = false;
        public bool VisibleEdit
        {
            get { return _visibleEdit; }
            set { SetProperty(ref _visibleEdit, value); }
        }
        bool _isEdit = false;
        public bool IsEdit
        {
            get { return _isEdit; }
            set { SetProperty(ref _isEdit, value); }
        }
        bool _isComment = false;
        public bool IsComment
        {
            get { return _isComment; }
            set { SetProperty(ref _isComment, value); }
        }
        bool _isEditReply = false;
        public bool IsEditReply
        {
            get { return _isEditReply; }
            set { SetProperty(ref _isEditReply, value); }
        }

        public ObservableCollection<Comments> CommentsList { get; set; } = new ObservableCollection<Comments>();
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CommentCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CommentEditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }



        private async Task DeleteData()
        {
            var comment = await _serviceComments.Delete<Model.Comments>(commentId);
            var updatedComments = new List<Comments>();
            for (int i = 0; i < CommentsList.Count(); i++)
            {
                if (CommentsList[i].id == commentId)
                {
                    CommentsList.Remove(CommentsList[i]);
                }
            }
            updatedComments.AddRange(CommentsList);
            CommentsList.Clear();
            foreach (var item in updatedComments)
            {
                CommentsList.Add(item);
            }
            IsEditReply = false;
            CommentPost = "";
        }

        public async Task LoadPostDetails(int id)
        {
            var userId = RestService.Session.id;
            var idFrom = id;
            string idPage = idFrom.ToString();
            var postsList = await _service.GetQuery<List<Post>>(new { id= idPage });
            var userList = await _serviceUsers.GetQuery<List<User>>(new { id = postsList[0].userId });
           
            string idPost = postsList[0].id.ToString();
            var commentsList = await _serviceComments.GetQuery<List<Comments>>(new { postId = idPost });

            if(postsList[0].userId == userId)
            {
                VisibleEdit = true;
            }
            TitlePost = postsList[0].title;
            User = userList[0].name;
            BodyPost = postsList[0].body;
            
            Date = DateTime.Now.Date.ToString();


            CommentsList.Clear();
            foreach (var comment in commentsList)
            {
                var result = comment.email.Substring(0, comment.email.IndexOf('@'));
                comment.email = result;
                CommentsList.Add(comment);
            }
        }
        public async Task SaveData()
        {
            if(IsEdit)
            {
                await UpdatePost();
            }
            if(IsComment)
            {
                await SaveComment();
            }
            if(IsEditReply)
            {
                await UpdateReply();
            }
            
        }
        private async Task SaveComment()
        {
            var request = new CommentInsertRequest
            {
                body = CommentPost,
                email = RestService.Session.email,
                postId = postId
            };
            var comment = await _serviceComments.Insert<Model.Comments>(request);
            comment.IsMine = true;
            CommentsList.Insert(0, comment);
            IsComment = false;
        }

        private async Task UpdatePost()
        {
            var request = new PostsUpdateRequest
            {
                body = BodyPost
            };
            var post = await _service.Patch<Model.Post>(postId, request);
            BodyPost = post.body;
            IsEdit = false;
        }
        private async Task UpdateReply()
        {
            var comment = await _serviceComments.Patch<Model.Comments>(commentId, new { body = CommentPost });
            var updatedComments = new List<Comments>();
            for (int i = 0; i < CommentsList.Count(); i++)
            {
                if(CommentsList[i].id == commentId)
                {
                    CommentsList[i].body = comment.body;
                }
            }
            updatedComments.AddRange(CommentsList);
            CommentsList.Clear();
            foreach(var item in updatedComments)
            {
                CommentsList.Add(item);
            }
            IsEditReply = false;
            CommentPost = "";
        }
        int commentId;
        private async Task EditComment(int id)
        {
            var comment = CommentsList.FirstOrDefault(x => x.id == id);

            CommentPost = comment.body;
            commentId = id;
            IsEditReply = true;
        }

        public async Task OnEdit()
        {
            IsEdit = true;
        }
        public async Task OnReply()
        {
            CommentPost = "";
            IsComment = true;
        }
      
        
    }
}
