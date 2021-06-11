using System;
using AirCampusTask.Models;

namespace AirCampusTask.ViewModels
{
    public class LearningTaskItemViewModel : ViewModel
    {
        public LearningTaskItemViewModel(LearningTask learningTask)
        {
            LearningTask = learningTask;
        }

        public LearningTask LearningTask { get; }
        public event EventHandler LearningTaskStatusChanged;
        public string StatusText => LearningTask.Completed ? "Reactivate" : "Completed";
        
    }
}