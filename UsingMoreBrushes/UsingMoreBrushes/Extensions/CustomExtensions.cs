using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using UsingMoreBrushes.ViewModels;

namespace UsingMoreBrushes.Extensions
{
    public static class CustomExtensions
    {
        /// <summary>
        /// Extension method <c>ToObservableCollection{T}</c> extends <see cref="IEnumerable{T}"/> to 
        /// convert it to an <see cref="ObservableCollection{T}"/> for a given type of T.
        /// </summary>
        /// <typeparam name="T">Generic Type of the IEnumerable collection.</typeparam>
        /// <param name="collection">Instance of an IEnumberable collection of type T</param>
        /// <returns>Instance of an ObservableCollection of type T</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var observableCollection = new ObservableCollection<T>();
            foreach (var item in collection)
            {
                observableCollection.Add(item);
            }
            return observableCollection;
        }

        /// <summary>
        /// Extension method <c>ToGradientStopCollection</c> extens <see cref="ObservableCollection{GradientStopViewModel}"/> to
        /// convert it to a <see cref="GradientStopCollection"/>
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static GradientStopCollection ToGradientStopCollection(this ObservableCollection<GradientStopViewModel> collection)
        {
            var gradientStopCollection = new GradientStopCollection();
            foreach (var item in collection)
            {
                gradientStopCollection.Add(new GradientStop(item.Colour.Color,item.Offset));
            }
            return gradientStopCollection;
        }
    }
}
