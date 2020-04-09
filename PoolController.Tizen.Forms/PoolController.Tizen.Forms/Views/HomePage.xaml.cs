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
                new HomeCellItem(CellType.Placeholder),
                new HomeCellItem(CellType.Placeholder),
                new HomeCellItem(CellType.Pool),
                new HomeCellItem(CellType.PoolLight),
                new HomeCellItem(CellType.Spa),
                new HomeCellItem(CellType.SpaLight),
                new HomeCellItem(CellType.Booster),
                new HomeCellItem(CellType.Heater),
                new HomeCellItem(CellType.GroundLights),
                new HomeCellItem(CellType.Placeholder),
                new HomeCellItem(CellType.Placeholder)
            };

            HomeItemsListview.ItemsSource = items;
        }

        private void HomeItemsListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is HomeCellItem item)
            {
                //stub
            }
        }
    }
}