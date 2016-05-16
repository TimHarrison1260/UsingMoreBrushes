using System.Windows;
using System.Windows.Media;
using UsingMoreBrushes.Builders;
using UsingMoreBrushes.Models;

namespace UsingMoreBrushes.Interfaces.Builders
{
    public interface IMyBrushBuilder
    {
        /*
         * All methods, which make up the "fluid" interface
         * must be included here
         */
        MyBrushBuilder Colour(ColourInfo colour);
        MyBrushBuilder StartPoint(Point startPoint);
        MyBrushBuilder EndPoint(Point endPoint);
        MyBrushBuilder RadiusX(double radiusX);
        MyBrushBuilder RadiusY(double radiusY);
        MyBrushBuilder Centre(Point centre);
        MyBrushBuilder GradientOrigin(Point gradientOrigin);
        MyBrushBuilder GradientStops(GradientStopCollection gradientStops);
        MyBrush Build();
    }
}
