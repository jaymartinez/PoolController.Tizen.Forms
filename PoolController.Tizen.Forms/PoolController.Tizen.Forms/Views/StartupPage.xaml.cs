using eHub.Common.Models;
using eHub.Common.Services;
using PoolController.Tizen.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoolController.Tizen.Forms.Views
{
	public partial class StartupPage : ContentPage
	{

		public StartupPage ()
		{
			InitializeComponent ();

            PoolControllerInjector.InjectProperties(this);
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            //var aic = new ActivityIndicatorControl();
            //await Navigation.PushModalAsync(aic);
            //var items = await GetInitialStatuses();
            //await Navigation.PopModalAsync();

            var homePage = new HomePage();
            ////homePage.SetData(items);
            await Navigation.PushAsync(homePage);
        }

       
    }
}