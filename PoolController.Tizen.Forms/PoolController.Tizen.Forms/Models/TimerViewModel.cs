using System;
using System.Collections.Generic;
using System.Text;

namespace PoolController.Tizen.Forms.Models
{
    public class TimerViewModel : BaseViewModel
    {
        TimeSpan _startTime = new TimeSpan(8, 30, 0);
        public TimeSpan TimerStartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(TimerStartTime));
            }
        }

        TimeSpan _endTime = new TimeSpan(14, 30, 0);
        public TimeSpan TimerEndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(TimerEndTime));
            }
        }
    }
}
