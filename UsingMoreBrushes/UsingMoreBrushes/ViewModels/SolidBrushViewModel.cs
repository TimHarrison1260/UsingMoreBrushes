using System;
using UsingMoreBrushes.Factories;
using UsingMoreBrushes.Interfaces.Factories;
using UsingMoreBrushes.Models;

namespace UsingMoreBrushes.ViewModels
{
    public class SolidBrushViewModel : BindableViewModelBase
    {
        public event EventHandler OnSolidColorBrushColorChanged;

        private readonly IColourInfoFactory _colourInfoFactory = new ColourInfoFactory();

        public SolidBrushViewModel()
        {
            _header = "Fill shape with a Colour";

            //var firstColourPropertyInfo = typeof (Colors).GetProperties().FirstOrDefault();
            //var firstColourName = firstColourPropertyInfo.Name;
            //var firstColour = (Color) firstColourPropertyInfo.GetValue(null);

            //_selectedColor = new ColourInfo
            //{
            //    Name = firstColourName,
            //    Color = firstColour
            //};

            _selectedColor = _colourInfoFactory.Create();       // Defaults to Blue

        }

        private string _header;
        public string Header
        {
            get { return _header; }
            private set
            {
                SetProperty(ref _header, value);
            }
        }

        private ColourInfo _selectedColor;
        public ColourInfo SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                SetProperty(ref _selectedColor, value);

                //  Raise event to allow handler to be triggered
                RaiseOnSolidColorBrushColorChanged();
            }
        }


        private void RaiseOnSolidColorBrushColorChanged()
        {
            if (OnSolidColorBrushColorChanged != null)
                OnSolidColorBrushColorChanged(this, new EventArgs());
        }



    }
}
