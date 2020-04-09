using eHub.Common.Models;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Models
{
    public class HomeCellItem : BaseViewModel
    {
        public CellType CellType { get; }
        public PiPin PiPin { get; }

        public string Name { get => CellType == CellType.Placeholder ? "" : CellType.ToString(); }

        int _state;
        public int State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public HomeCellItem(PiPin pin, CellType cellType)
        {
            CellType = cellType;

            if (pin != null)
            {
                PiPin = pin;
                State = pin.State;
            }
        }
    }

    public enum CellType
    {
        Schedule,
        Pool,
        Spa,
        Booster,
        Heater,
        GroundLights,
        SpaLight,
        PoolLight,
        About,
        Placeholder
    }
}