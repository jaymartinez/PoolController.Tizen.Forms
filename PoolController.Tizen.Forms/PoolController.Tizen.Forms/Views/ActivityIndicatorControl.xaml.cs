using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoolController.Tizen.Forms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivityIndicatorControl : ContentPage
	{
		public ActivityIndicatorControl ()
		{
			InitializeComponent ();
		}
	}
}