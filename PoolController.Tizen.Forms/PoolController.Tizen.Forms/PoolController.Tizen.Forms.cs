using eHub.Common.Api;
using eHub.Common.Models;
using eHub.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PoolController.Tizen.Forms
{
    public class App : Application
    {
        public App()
        {
            Initialize();

            //Configuration config = new Configuration()
            //WebInterface webApi = new WebInterface();
            //PoolApi api = new PoolApi();
            //PoolService ps = new PoolService();
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Tizen Xamarin!!!"
                        },
                        new Button
                        {
                            Text = "Click Me",
                            Command = new Command(() =>
                            {
                                Console.WriteLine("Button Tapped!");
                            })
                        }
                    }
                }
            };
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

        }
    }
}
