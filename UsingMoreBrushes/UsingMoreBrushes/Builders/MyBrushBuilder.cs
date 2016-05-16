using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using UsingMoreBrushes.Interfaces.Builders;
using UsingMoreBrushes.Models;

namespace UsingMoreBrushes.Builders
{
    public class MyBrushBuilder : IMyBrushBuilder
    {
        private readonly string _brushName;

        private ColourInfo _colour;

        private Point _startPoint;
        private Point _endPoint;

        private double _radiusX;
        private double _radiusY;
        private Point _centre;
        private Point _gradientOrigin;

        private GradientStopCollection _gradientStops;

        public MyBrushBuilder(string brushName)
        {
            if (string.IsNullOrWhiteSpace(brushName))
                throw new ArgumentNullException(nameof(brushName));
            _brushName = brushName;
        }


        public MyBrushBuilder Colour(ColourInfo colour)
        {
            this._colour = colour;
            return this;
        }

        public MyBrushBuilder StartPoint(Point startPoint)
        {
            _startPoint = startPoint;
            return this;
        }

        public MyBrushBuilder EndPoint(Point endPoint)
        {
            _endPoint = endPoint;
            return this;
        }

        public MyBrushBuilder RadiusX(double radiusX)
        {
            _radiusX = radiusX;
            return this;
        }

        public MyBrushBuilder RadiusY(double radiusY )
        {
            _radiusY = radiusY;
            return this;
        }

        public MyBrushBuilder Centre(Point centre)
        {
            _centre = centre;
            return this;
        }

        public MyBrushBuilder GradientOrigin(Point gradientOrigin)
        {
            _gradientOrigin = gradientOrigin;
            return this;
        }

        public MyBrushBuilder GradientStops(GradientStopCollection gradientStops)
        {
            _gradientStops = gradientStops.Any() ? gradientStops : new GradientStopCollection();
            return this;
        }

        /// <summary>
        /// Method Build() allows the use of the "var" keyword so that the object created is
        /// of the correct type, namely <see cref="MyBrush"/>. This is an in-built conversion
        /// which does not allow explicit Casts to be done.  Explicit converters would need to
        /// be defined to allow explicit casts to be performed on the objects.
        /// </summary>
        /// <returns></returns>
        public MyBrush Build()
        {
            var myBrush = new MyBrush(_brushName, _colour, _startPoint, _endPoint, _radiusX, _radiusY, _centre, _gradientOrigin, _gradientStops);
            return myBrush;
        }

        /// <summary>
        /// Operator MyBrush removes the need to call the Build() method explicitly by defining
        /// the conversion operator between MyBrushBuilder and MyBrush.
        /// </summary>
        /// <param name="bldr">This instance of the <see cref="MyBrushBuilder"/> class.</param>
        public static implicit operator MyBrush(MyBrushBuilder bldr)
        {
            return new MyBrush(
                bldr._brushName,
                bldr._colour,
                bldr._startPoint,
                bldr._endPoint,
                bldr._radiusX,
                bldr._radiusY,
                bldr._centre,
                bldr._gradientOrigin,
                bldr._gradientStops);
        }

    }

}
