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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
            InitializeComponent();

            HomeItemsListview.ItemsSource = new List<HomeCellItem>
            {
                new HomeCellItem(CellType.Pool)
            };

        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
	}
}