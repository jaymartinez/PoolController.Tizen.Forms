using eHub.Common.Models;
using eHub.Common.Services;
using PoolController.Tizen.Forms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                else
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
                new HomeCellItem(new PiPin(), CellType.Refresh),
                poolItem,
                poolLightItem,
                spaItem,
                spaLightItem,
                boosterItem,
                heaterItem,
                groundLightsItem
            };

            HomeItemsListview.ItemsSource = items;

            var sched = await PoolService.GetSchedule();

            if (sched != null)
            {
                var start = new TimeSpan(sched.StartHour, sched.StartMinute, 0);
                var end = new TimeSpan(sched.EndHour, sched.EndMinute, 0);

                //var timerModel = new TimerViewModel();
                StartTime.Time = start;//BindingContext = timerModel;
                EndTime.Time = end;//BindingContext = timerModel;
                //timerModel.TimerStartTime = start;
                //timerModel.TimerEndTime = end;

                PoolTimerSwitch.IsToggled = sched.IsActive;
                //PoolTimerSwitch.BindingContext = sched;
                //OnPropertyChanged(nameof(sched.IsActive));
            }

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

        async void StartTime_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName ==  "Time")
            {
                var startPicker = sender as TimePicker;
                if (startPicker == null)
                    return;

                var st = startPicker.Time;
                var et = EndTime.Time;
                var active = PoolTimerSwitch.IsToggled;

                await SaveSchedule(st, et, active);
            }
        }

        async void EndTime_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                var endPicker = sender as TimePicker;
                if (endPicker == null)
                    return;

                var et = endPicker.Time;
                var st = StartTime.Time;
                var active = PoolTimerSwitch.IsToggled;

                await SaveSchedule(st, et, active);
            }
        }

        async Task SaveSchedule(TimeSpan start, TimeSpan end, bool active)
        {
            var s = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                start.Hours,
                start.Minutes,
                0);

            var e = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                end.Hours,
                end.Minutes,
                0);

            var result = await PoolService.SetSchedule(s, e, active);
            if (result == null)
            {
                // If save was unsuccessful then reload the page to get correct values.
                await Reload();
            }
        }

        async void PoolTimerSwitch_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsToggled")
            {
                var sw = sender as Switch;
                if (sw == null)
                    return;

                var active = sw.IsToggled;
                var st = StartTime.Time;
                var et = EndTime.Time;

                await SaveSchedule(st, et, active);
            }
        }
    }
}