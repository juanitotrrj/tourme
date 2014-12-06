using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private MakatiTourViewModel _makatiTourModel;
       private ManilaTourViewModel _manilaTourModel;
       private PasayTourViewModel _pasayTourModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = MakatiTourModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public MakatiTourViewModel MakatiTourModel
        {
            get { return _makatiTourModel ?? (_makatiTourModel = new MakatiTourViewModel()); }
        }
 
        public ManilaTourViewModel ManilaTourModel
        {
            get { return _manilaTourModel ?? (_manilaTourModel = new ManilaTourViewModel()); }
        }
 
        public PasayTourViewModel PasayTourModel
        {
            get { return _pasayTourModel ?? (_pasayTourModel = new PasayTourViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            MakatiTourModel.ViewType = viewType;
            ManilaTourModel.ViewType = viewType;
            PasayTourModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

      get { return Visibility.Collapsed; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                MakatiTourModel.LoadItemsAsync(forceRefresh),
                ManilaTourModel.LoadItemsAsync(forceRefresh),
                PasayTourModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
