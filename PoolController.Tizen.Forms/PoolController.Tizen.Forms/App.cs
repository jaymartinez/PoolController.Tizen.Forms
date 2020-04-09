using Autofac;
using eHub.Common.Services;
using PoolController.Tizen.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms
{
    public class App : Application
    {
        static IContainer _container;
        public IContainer Container => _container;

        IPoolService _poolService;

        public App()
        {
            Initialize();

            var mainPage = new HomePage();

            Task.Run(async () =>
            {
                await mainPage.Page_Init();
                MainPage = mainPage;
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
