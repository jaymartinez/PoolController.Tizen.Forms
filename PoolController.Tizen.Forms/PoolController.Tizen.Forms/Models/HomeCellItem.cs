using eHub.Common.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PoolController.Tizen.Forms.Models
{
    public class HomeCellItem
    {
        public CellType CellType { get; }
        public string Name { get => CellType == CellType.Placeholder ? "" : CellType.ToString(); }

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
        About,
        Placeholder
    }
}