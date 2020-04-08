using eHub.Common.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Models
{
    public class HomeCellItem
    {
        public CellType CellType { get; }
        public string Name { get => CellType.ToString(); }
        public ICommand ButtonTapCommand { get; private set; }

        public HomeCellItem(CellType cellType)
        {
            ButtonTapCommand = new Command(item =>
            {
                if (item is HomeCellItem i)
                {
                    switch (i.CellType)
                    {
                        case CellType.Pool:
                            break;
                        case CellType.Spa:
                            break;
                        case CellType.Booster:
                            break;
                        case CellType.Heater:
                            break;
                        case CellType.GroundLights:
                            break;
                        case CellType.PoolLight:
                            break;
                        case CellType.SpaLight:
                            break;
                    }
                }
            });


            CellType = cellType;
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
        About
    }
}