using eHub.Common.Models;
using eHub.Common.Services;
using PoolController.Tizen.Forms.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Views
{
	public partial class HomePage : ContentPage
	{
        [Inject] IPoolService PoolService { get; set; }

        public HomePage()
        {
            InitializeComponent();
            PoolControllerInjector.InjectProperties(this);
        }

        async void HomeItemsListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is HomeCellItem item)
            {
                if (item.CellType == CellType.Refresh)
                {
                    await Reload();
                }
                else if (item.CellType != CellType.Placeholder)
                {
                    var resultStatus = default(PiPin);
                    switch (item.CellType)
                    {
                        case CellType.Pool:
                            var heaterStatus = await GetStatus(Pin.Heater);
                            var boosterStatus = await GetStatus(Pin.BoosterPump);
                            var curPoolState = await GetStatus(Pin.PoolPump);

                            if (curPoolState.State == PinState.ON
                                && (heaterStatus.State == PinState.ON || boosterStatus.State == PinState.ON))
                            {
                                await DisplayAlert("", "The heater and booster must be off first!", "Ok");
                                return;
                            }
                            var onoff = curPoolState.State == PinState.ON ? "OFF" : "ON";
                            var confirm = await DisplayAlert("You Sure?", $"Turn it {onoff} ?", "Yes", "No");
                            if (confirm)
                            {
                                resultStatus = await PoolService.Toggle(Pin.PoolPump);
                            }
                            else return;
                            break;
                        case CellType.PoolLight:
                            resultStatus = await PoolService.Toggle(Pin.PoolLight);
                            break;
                        case CellType.SpaLight:
                            resultStatus = await PoolService.Toggle(Pin.SpaLight);
                            break;
                        case CellType.GroundLights:
                            resultStatus = await PoolService.Toggle(Pin.GroundLights);
                            break;
                        case CellType.Spa:
                            resultStatus = await PoolService.Toggle(Pin.SpaPump);
                            break;
                        case CellType.Heater:
                            heaterStatus = await GetStatus(Pin.Heater);
                            curPoolState = await GetStatus(Pin.PoolPump);

                            // Make sure the pool pump is on first!
                            if (heaterStatus.State == PinState.OFF && curPoolState.State == PinState.OFF)
                            {
                                await DisplayAlert("Wait!", "The pool pump needs to be on first!", "Ok");
                                return;
                            }

                            resultStatus = await PoolService.Toggle(Pin.Heater);
                            break;
                        case CellType.Booster:
                            boosterStatus = await GetStatus(Pin.BoosterPump);
                            curPoolState = await GetStatus(Pin.PoolPump);
                            if (boosterStatus.State == PinState.OFF && curPoolState.State == PinState.OFF)
                            {
                                await DisplayAlert("Wait!", "The pool pump needs to be on first!", "Ok");
                                return;
                            }

                            resultStatus = await PoolService.Toggle(Pin.BoosterPump);
                            break;
                    }

                    if (resultStatus != null)
                        item.State = resultStatus.State;
                }
            }
        }

        public async Task Reload()
        {
            var aic = new ActivityIndicatorControl();
            await Navigation.PushModalAsync(aic);

            var pinged = await PoolService.Ping();
            if (!pinged)
            {
                StatusLabel.IsVisible = true;
                HomeItemsListview.IsVisible = false;
                await Navigation.PopModalAsync();
                return;
            }

            HomeItemsListview.IsVisible = true;
            StatusLabel.IsVisible = false;

            HomeCellItem poolItem = null;
            HomeCellItem spaItem = null;
            HomeCellItem poolLightItem = null;
            HomeCellItem spaLightItem = null;
            HomeCellItem boosterItem = null;
            HomeCellItem heaterItem = null;
            HomeCellItem groundLightsItem = null;

            try
            {
                var poolLight = await GetStatus(Pin.PoolLight);
                poolLightItem = new HomeCellItem(poolLight, CellType.PoolLight);
            }
            catch (Exception e)
            {
                poolLightItem = new HomeCellItem(new PiPin(), CellType.PoolLight, e.Message);
            }
            try
            {
                var spaLight = await GetStatus(Pin.SpaLight);
                spaLightItem = new HomeCellItem(spaLight, CellType.SpaLight);
            }
            catch (Exception e)
            {
                spaLightItem = new HomeCellItem(new PiPin(), CellType.SpaLight, e.Message);
            }
            try
            {
                var pool = await GetStatus(Pin.PoolPump);
                poolItem = new HomeCellItem(pool, CellType.Pool);
            }
            catch (Exception e)
            {
                poolItem = new HomeCellItem(new PiPin(), CellType.Pool, e.Message);
            }
            try
            {
                var spa = await GetStatus(Pin.SpaPump);
                spaItem = new HomeCellItem(spa, CellType.Spa);
            }
            catch (Exception e)
            {
                spaItem = new HomeCellItem(new PiPin(), CellType.Spa, e.Message);
            }
            try
            {
                var booster = await GetStatus(Pin.BoosterPump);
                boosterItem = new HomeCellItem(booster, CellType.Booster);
            }
            catch (Exception e)
            {
                boosterItem = new HomeCellItem(new PiPin(), CellType.Booster, e.Message);
            }
            try
            {
                var heater = await GetStatus(Pin.Heater);
                heaterItem = new HomeCellItem(heater, CellType.Heater);
            }
            catch (Exception e)
            {
                heaterItem = new HomeCellItem(new PiPin(), CellType.Heater, e.Message);
            }
            try
            {
                var groundLights = await GetStatus(Pin.GroundLights);
                groundLightsItem = new HomeCellItem(groundLights, CellType.GroundLights);
            }
            catch (Exception e)
            {
                groundLightsItem = new HomeCellItem(new PiPin(), CellType.GroundLights, e.Message);
            }

            var items = new List<HomeCellItem>
            {
                new HomeCellItem(new PiPin(), CellType.Placeholder),
                new HomeCellItem(new PiPin(), CellType.Refresh),
                poolItem,
                poolLightItem,
                spaItem,
                spaLightItem,
                boosterItem,
                heaterItem,
                groundLightsItem,
                new HomeCellItem(new PiPin(), CellType.Placeholder),
                new HomeCellItem(new PiPin(), CellType.Placeholder)
            };

            HomeItemsListview.ItemsSource = items;
            await Navigation.PopModalAsync();
        }

        async Task<PiPin> GetStatus(int pin)
        {
            try
            {
                return await PoolService.GetPinStatus(pin);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Reload();
        }
    }
}