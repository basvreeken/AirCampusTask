using System.ComponentModel;
using Xamarin.Forms;

namespace AirCampusTask.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        // todo: Abstract the Navigation from the viewmodel.
        public INavigation Navigation { get; set; }
    }
}