using System.Collections.ObjectModel;
using UsingMoreBrushes.Models;
using UsingMoreBrushes.ViewModels;

namespace UsingMoreBrushes.Interfaces.Data
{
    public interface IDataManager
    {
        ObservableCollection<ShapeViewModel> GetShapeViewModels();

        ObservableCollection<string> GetShapeNames();

        ObservableCollection<string> GetBrushNames();

        ObservableCollection<ColourInfo> GetColors();
    }
}
