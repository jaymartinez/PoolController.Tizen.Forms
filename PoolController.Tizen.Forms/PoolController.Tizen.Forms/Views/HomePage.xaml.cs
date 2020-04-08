using PoolController.Tizen.Forms.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Views
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
            InitializeComponent();

            var items = new List<HomeCellItem>
            {
                new HomeCellItem(CellType.Pool),
                new HomeCellItem(CellType.Spa),
                new HomeCellItem(CellType.Booster),
                new HomeCellItem(CellType.Heater),
                new HomeCellItem(CellType.GroundLights)
            };

            HomeItemsListview.ItemsSource = items;
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}