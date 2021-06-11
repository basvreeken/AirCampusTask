using System;
using AirCampusTask.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace AirCampusTask
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // todo: Replace this old way for the new Shell
            MainPage = new NavigationPage(Resolver.Resolve<MainView>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}