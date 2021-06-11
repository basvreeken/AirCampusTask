using System;
using System.Windows.Input;
using AirCampusTask.Models;
using AirCampusTask.Repositories;
using Xamarin.Forms;

namespace AirCampusTask.ViewModels
{
    public class LearningTaskViewModel : ViewModel
    {
        private readonly LearningTaskRepository _repository;
        public LearningTask LearningTask { get; set; }

        public LearningTaskViewModel(LearningTaskRepository repository)
        {
            _repository = repository;
            LearningTask = new LearningTask() {Due = DateTime.Now.AddDays(1)};
        }

        public ICommand Save => new Command(async () =>
        {
            await _repository.AddOrUpdate(LearningTask);
            await Navigation.PopAsync();
        });
    }
}