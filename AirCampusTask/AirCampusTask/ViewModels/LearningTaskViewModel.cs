using AirCampusTask.Models;
using AirCampusTask.Repositories;

namespace AirCampusTask.ViewModels
{
    public class LearningTaskViewModel : ViewModel
    {
        private readonly LearningTaskRepository _repository;

        public LearningTaskViewModel(LearningTaskRepository repository)
        {
            _repository = repository;
        }
        
    }
}