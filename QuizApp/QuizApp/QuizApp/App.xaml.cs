using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using QuizApp.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace QuizApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
	        MainPage = new NavigationPage();
	        var kernel = new StandardKernel();
	        Bootstrapper._initialize(kernel);
	        Resolver = kernel;
        }

        public static StandardKernel Resolver { get; private set; }

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
