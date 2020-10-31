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
    public class GalleryPageViewModel : ViewModelBase
    {

        public GalleryPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        

    }
}
