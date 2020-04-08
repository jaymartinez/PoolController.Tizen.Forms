using System;

namespace PoolController.Tizen.Forms.Models
{
    [Serializable]
    public class TimePickerArgs
    {
        public int Hour { get; }
        public int Minute { get; }

        public TimePickerArgs(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
    }
}