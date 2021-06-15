using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AirCampusTask.Models;
using AirCampusTask.Repositories;
using AirCampusTask.Views;
using Xamarin.Forms;

namespace AirCampusTask.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly LearningTaskRepository _repository;
        public ObservableCollection<LearningTaskItemViewModel> TaskBook { get; set; }

        public LearningTaskItemViewModel SelectedLearningTask
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToLearningTask(value));
                RaisePropertyChanged(nameof(SelectedLearningTask));
            }
        }

        private async Task NavigateToLearningTask(LearningTaskItemViewModel learningTaskItemViewModel)
        {
            if (learningTaskItemViewModel == null)
            {
                return;
            }

            var learningTaskView = Resolver.Resolve<LearningTaskView>();
            var vm = learningTaskView.BindingContext as LearningTaskViewModel;
            vm.LearningTask = learningTaskItemViewModel.LearningTask;

            await Navigation.PushAsync(learningTaskView);

        }

        public MainViewModel(LearningTaskRepository repository)
        {
            repository.OnLearningTaskAdded += (sender, learningTask) =>
                TaskBook.Add(CreateLearningTaskItemViewModel(learningTask));
            repository.OnLearningTaskUpdated += (sender, learningTask) =>
                Task.Run(async () => await LoadData());
            
            _repository = repository;
            
            Task.Run(async () => await LoadData());
        }

        public ICommand AddLearningTask => new Command(async () =>
        {
            var learningTaskView = Resolver.Resolve<LearningTaskView>();
            await Navigation.PushAsync(learningTaskView);
        });

        private async Task LoadData()
        {
            var learningTasks = await _repository.GetLearningTasks();

            if (!ShowAll)
            {
                learningTasks = learningTasks.Where(l => l.Synced == false).ToList();
            }
            
            var learningTaskItemViewModels = learningTasks.Select(l => CreateLearningTaskItemViewModel(l));
            TaskBook = new ObservableCollection<LearningTaskItemViewModel>(learningTaskItemViewModels);
        }

        private LearningTaskItemViewModel CreateLearningTaskItemViewModel(LearningTask learningTask)
        {
            var learningTaskItemViewModel = new LearningTaskItemViewModel(learningTask);
            learningTaskItemViewModel.LearningTaskStatusChanged += LearningTaskStatusChanged;
            return learningTaskItemViewModel;
        }

        private void LearningTaskStatusChanged(object sender, EventArgs e)
        {
            if (sender is LearningTaskItemViewModel learningTaskItemViewModel)
            {
                if (!ShowAll && learningTaskItemViewModel.LearningTask.Synced)
                {
                    TaskBook.Remove(learningTaskItemViewModel);
                }

                Task.Run(async () => await _repository.UpdateLearningTask(learningTaskItemViewModel.LearningTask));
            }
        }
        
        public bool ShowAll { get; set; }
        public string FilterText => ShowAll ? "All tasks" : "Active tasks";

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });
        
    }
}