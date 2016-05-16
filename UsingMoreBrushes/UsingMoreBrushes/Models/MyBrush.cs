using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using UsingMoreBrushes.Interfaces.Models;

namespace UsingMoreBrushes.Models
{
    public class MyBrush : IMyBrush
    {
        #region fields / properties

        /*
         * Depending on the brush being built, groups of properties
         * are required and the rest are options (or not required)
         */

        private readonly string _brushName;

        public ColourInfo Colour { get; private set; }

        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }

        public double RadiusX { get; private set; }
        public double RadiusY { get; private set; }
        public Point Centre { get; private set; }
        public Point GradientOrigin { get; private set; }

        public GradientStopCollection GradientStops { get; private set; }

        #endregion

        public MyBrush(string brushName, ColourInfo colour, Point startPoint, Point endPoint, double radiusX, double radiusY, Point centre,
            Point gradientOrigin, GradientStopCollection gradientStops)
        {
            //  BrushName is a required property
            if (string.IsNullOrWhiteSpace(brushName)) throw new ArgumentNullException(nameof(brushName), "Cannot create brush without a valid Name.");
            _brushName = brushName;
            //  All other properties are optional, they are only required for specific types of Brushes
            this.Colour = colour ?? new ColourInfo();
            this.StartPoint = startPoint;
            EndPoint = endPoint;
            RadiusX = radiusX;
            RadiusY = radiusY;
            Centre = centre;
            GradientOrigin = gradientOrigin;
            GradientStops = gradientStops;
        }

        /// <summary>
        /// Gets an instance of the correct type of <see cref="System.Windows.Media.Brush"/>.
        /// </summary>
        public Brush Brush
        {
            get
            {
                if (_brushName == typeof(SolidColorBrush).Name)
                    return new SolidColorBrush(Colour.Color);

                if (_brushName == typeof (LinearGradientBrush).Name)
                {
                    return new LinearGradientBrush
                    {
                        StartPoint = StartPoint,
                        EndPoint = EndPoint,
                        GradientStops = GradientStops
                    };
                }

                if (_brushName == typeof (RadialGradientBrush).Name)
                {
                    return new RadialGradientBrush
                    {
                        RadiusX = RadiusX,
                        RadiusY = RadiusY,
                        GradientOrigin = GradientOrigin,
                        Center = Centre,
                        GradientStops = GradientStops
                    };
                }

                //  Return an arbitrary Blue SolidColorBrush, if Brush not supported
                return new SolidColorBrush(Colors.Blue);
            }
        }

    }


}
