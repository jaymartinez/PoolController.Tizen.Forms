using eHub.Common.Models;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Models
{
    public class HomeCellItem : BaseViewModel
    {
        public CellType CellType { get; }
        public PiPin PiPin { get; }

        public string Name { get => CellType.ToString(); }

        int _state;
        public int State
        {
            get => _state;
            set
            {
                _state = value;

                if (_state == PinState.ON)
                {
                    BGColor = Color.LightGreen;
                    BtnTextColor = Color.DarkBlue;
                }
                else
                {
                    BGColor = Color.Default;
                    BtnTextColor = Color.White;
                }

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

            if (CellType == CellType.Refresh)
            {
                BGColor = Color.White;
                BtnTextColor = Color.DarkBlue;
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
        Refresh
    }
}