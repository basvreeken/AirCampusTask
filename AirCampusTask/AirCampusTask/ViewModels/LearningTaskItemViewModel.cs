using System;
using System.Windows.Input;
using AirCampusTask.Models;
using Xamarin.Forms;

namespace AirCampusTask.ViewModels
{
    public class LearningTaskItemViewModel : ViewModel
    {
        public LearningTaskItemViewModel(LearningTask learningTask)
        {
            LearningTask = learningTask;
        }

        public ICommand ToggleCompleted => new Command((arg) =>
        {
            LearningTask.Synced = !LearningTask.Synced;
            LearningTaskStatusChanged?.Invoke(this, new EventArgs());
        });
        public LearningTask LearningTask { get; }
        public event EventHandler LearningTaskStatusChanged;

        // todo: Remove logic by using a value converter.
        public string StatusText => LearningTask.Synced ? "Reactivate" : "Synced";
        
        
    }
}