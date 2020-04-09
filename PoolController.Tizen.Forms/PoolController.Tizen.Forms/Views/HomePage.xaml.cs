using eHub.Common.Models;
using eHub.Common.Services;
using PoolController.Tizen.Forms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Views
{
	public partial class HomePage : ContentPage
	{
        [Inject] IPoolService PoolService { get; set; }

		public HomePage ()
		{
            InitializeComponent();

            PoolControllerInjector.InjectProperties(this);
        }

        public async Task Page_Init()
        {
            var pool = await GetStatus(Pin.PoolPump);
            var poolLight = await GetStatus(Pin.PoolLight);
            var spa = await GetStatus(Pin.SpaPump);
            var spaLight = await GetStatus(Pin.SpaLight);
            var booster = await GetStatus(Pin.BoosterPump);
            var heater = await GetStatus(Pin.Heater);
            var groundLights = await GetStatus(Pin.GroundLights);

            var items = new List<HomeCellItem>
            {
                new HomeCellItem(null, CellType.Placeholder),
                new HomeCellItem(null, CellType.Placeholder),
                new HomeCellItem(pool, CellType.Pool),
                new HomeCellItem(poolLight, CellType.PoolLight),
                new HomeCellItem(spa, CellType.Spa),
                new HomeCellItem(spaLight, CellType.SpaLight),
                new HomeCellItem(booster, CellType.Booster),
                new HomeCellItem(heater, CellType.Heater),
                new HomeCellItem(groundLights, CellType.GroundLights),
                new HomeCellItem(null, CellType.Placeholder),
                new HomeCellItem(null, CellType.Placeholder)
            };

            HomeItemsListview.ItemsSource = items;
        }

        async Task<PiPin> GetStatus(int pin)
        {
            return await PoolService.GetPinStatus(pin);
        }

        void HomeItemsListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is HomeCellItem item)
            {
                //stub
            }
        }
    }
}