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
        public Color BGColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BGColor));
            }
        }

        Color _btnTextColor;
        public Color BtnTextColor
        {
            get => _btnTextColor;
            set
            {
                _btnTextColor = value;
                OnPropertyChanged(nameof(BtnTextColor));
            }
        }

        bool _isErrorLabelVisible;
        public bool IsErrorLabelVisible
        {
            get => _isErrorLabelVisible;
            set
            {
                _isErrorLabelVisible = value;
                OnPropertyChanged(nameof(IsErrorLabelVisible));
            }
        }

        string _errorText;
        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }

        public HomeCellItem(PiPin pin, CellType cellType, string errorMsg = null)
        {
            PiPin = pin;
            State = pin.State;
            CellType = cellType;

            if (State == PinState.ON)
            {
                BGColor = Color.LightGreen;
                BtnTextColor = Color.DarkBlue;
            }
            else
            {
                if (CellType != CellType.Placeholder)
                    BGColor = Color.Default;
                else
                    BGColor = Color.Transparent;

                BtnTextColor = Color.White;
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                ErrorText = errorMsg;
                IsErrorLabelVisible = true;
            }
            else
            {
                IsErrorLabelVisible = false;
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