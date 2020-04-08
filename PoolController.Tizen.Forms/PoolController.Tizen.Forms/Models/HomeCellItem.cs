using eHub.Common.Models;
using System;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Models
{
    public class HomeCellItem
    {
        public CellType CellType { get; }
        public string Name { get => CellType.ToString(); }

        public Command<HomeCellItem> ButtonTapCommand => 
            new Command<HomeCellItem>(item =>
            {
                switch (item.CellType)
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
            });


        public HomeCellItem(CellType cellType)
        {
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