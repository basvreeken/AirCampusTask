using System.Threading.Tasks;
using AirCampusTask.Repositories;

namespace AirCampusTask.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly LearningTaskRepository _repository;

        public MainViewModel(LearningTaskRepository repository)
        {
            _repository = repository;
            Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {

        }
    }
}