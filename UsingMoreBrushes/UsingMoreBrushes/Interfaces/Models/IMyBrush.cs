using System.Windows.Media;
using UsingMoreBrushes.Models;  // used in XML comments

namespace UsingMoreBrushes.Interfaces.Models
{
    public interface IMyBrush
    {
        /// <summary>
        /// Gets the instance of the Brush contained in <see cref="MyBrush"/> class.
        /// </summary>
        Brush Brush { get; }
    }
}
