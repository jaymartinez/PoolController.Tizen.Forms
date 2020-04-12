using Autofac;
using eHub.Common.Services;
using PoolController.Tizen.Forms.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms
{
    public class App : Application
    {
        static IContainer _container;
        public IContainer Container => _container;

        public App()
        {
            Initialize();

            Task.Run(async () =>
            {
                var homePage = new HomePage();
                await homePage.Reload();
                MainPage = homePage;
            });

            // The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Button
            //            {
            //                Text = "Pool Light",
            //                Command = new Command(async () =>
            //                {
            //                }) 
            //            },
            //            new Button
            //            {
            //                Text = "Spa Light",
            //                Command = new Command(async () =>
            //                {
            //                })
            //            }
            //        }
            //    }
            //};
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

        void Initialize()
        {
            _container = new ContainerConfig().Configure();
        }
    }
}
