using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using UsingMoreBrushes.Builders;
using UsingMoreBrushes.Data;
using UsingMoreBrushes.Extensions;
using UsingMoreBrushes.Interfaces.Builders;
using UsingMoreBrushes.Interfaces.Data;
using UsingMoreBrushes.Interfaces.Models;
using UsingMoreBrushes.Models;

namespace UsingMoreBrushes.ViewModels
{
    public class MainWindowViewModel : BindableViewModelBase
    {
        #region Fields

        private readonly IDataManager _manager;     //  Accesses the data
        //private readonly ICreateBrushHelper _brushHelper;   //  Builder for creating Media.Brush instances

        #endregion


        public MainWindowViewModel()
        {
            _manager = new DataManager();
            
            //  Load the Data
            
            //  Define the Shapes that can be shown
            _shapesNames = _manager.GetShapeNames();
            /*
             * Leave this null check out and the datacontext throws an error in the designtime editor
             * "Value cannot be null, Parameter Name: Source", when the XAML references this
             * ViewModel.
             */
            if (_shapesNames != null)
                _selectedShapeName = _shapesNames.FirstOrDefault();
            ToggleVisibility(_selectedShapeName);


            //  Define Colours that can be used
            //var colours = typeof (Colors).GetRuntimeProperties();
            //_coloursCollection = colours.ToObservableCollection();
            _coloursCollection = _manager.GetColors();


            //  Define the SolidBrush properties, which also set the SelectedColour
            _solidBrushProperties = new SolidBrushViewModel();
            //  Attach ColorChanged event handler
            _solidBrushProperties.OnSolidColorBrushColorChanged += SolidBrushPropertiesOnOnSolidColorBrushColorChanged;

            //  Define the LinearGradientBrush properties
            _linearGradientBrushProperties = new LinearGradientBrushViewModel();
            _linearGradientBrushProperties.OnLinearGradientBrushPropertiesChanged += LinearGradientBrushPropertiesOnOnLinearGradientBrushPropertiesChanged;

            //  Define the RadialGradientBrush properties
            //  Todo: Instantiate the brush view model class and attach the event handler
            _radialGradientBrushProperties = new RadialGradientBrushViewModel();
            _radialGradientBrushProperties.OnRadialGradientBrushPropertiesChanged += RadialGradientBrushPropertiesOnOnRadialGradientBrushPropertiesChanged;

            //  Define the Brushes that can be used
            _brushNames = _manager.GetBrushNames();
            _selectedBrushName = _brushNames.FirstOrDefault();
            _selectedBrush = CreateBrush(_selectedBrushName);   // Requires the Brush Properties to be set
            ToggleBrushPropertiesVisibility(_selectedBrushName);
        }


        #region Properties

        /* The Shapes ComboBox stuff
         */
        private readonly ObservableCollection<string> _shapesNames;
        public ObservableCollection<string> ShapesNames
        {
            get { return _shapesNames; }
        }

        private string _selectedShapeName;
        public string SelectedShapeName
        {
            get { return _selectedShapeName; }
            set
            {
                SetProperty(ref _selectedShapeName, value);

                //  Swap to the selected shape for displaying
                ToggleVisibility(_selectedShapeName);
            }
        }

        /* 
         * Visibility for the shapes, used to toggles the shape
         * currently visible within the ViewBox
         */
        private bool _rectangleIsVisible;
        public bool RectangleIsVisible
        {
            get { return _rectangleIsVisible; }
            private set
            {
                SetProperty(ref _rectangleIsVisible, value);
            }
        }

        private bool _ellipseIsVisible;
        public bool EllipseIsVisible
        {
            get { return _ellipseIsVisible; }
            private set
            {
                SetProperty(ref _ellipseIsVisible, value);
            }
        }

        /* 
         * Brush Combobox and selected Brush properties
         */
        private ObservableCollection<string> _brushNames;
        public ObservableCollection<string> BrushNames
        {
            get { return _brushNames; }
        }

        private string _selectedBrushName;
        public string SelectedBrushName
        {
            get { return _selectedBrushName; }
            set
            {
                SetProperty(ref _selectedBrushName, value);

                /*  
                 * Trigger the logic to create the appropriate brush 
                 * and update the selectedBrush itself.
                 */
                //  Update the selected Brush with the correct properties
                this.SelectedBrush = CreateBrush(_selectedBrushName);

                //  show the properties of the selected brush
                ToggleBrushPropertiesVisibility(_selectedBrushName);
            }
        }

        private Brush _selectedBrush;
        public Brush SelectedBrush
        {
            get { return _selectedBrush; }
            private set
            {
                SetProperty(ref _selectedBrush, value);
            }
        }


        /* 
         * Colours for the Colour selection ComboBoxes, so that
         * we only have on instance of them (256 pre-defined colours).
         */
        private ObservableCollection<ColourInfo> _coloursCollection;
        public ObservableCollection<ColourInfo> ColoursCollection
        { get { return _coloursCollection; } }




        /*
         * Properties, ViewModels for the properties of each
         * Brush Type.
         */
        private SolidBrushViewModel _solidBrushProperties;
        public SolidBrushViewModel SolidBrushProperties
        {
            get { return _solidBrushProperties; }
            set { SetProperty(ref _solidBrushProperties, value);}
        }

        private bool _isSolidBrushPropertiesVisible;
        public bool IsSolidBrushPropertiesVisible
        {
            get { return _isSolidBrushPropertiesVisible; }
            private set { SetProperty(ref _isSolidBrushPropertiesVisible, value);}
        }

        private void SolidBrushPropertiesOnOnSolidColorBrushColorChanged(object sender, EventArgs eventArgs)
        {
            this.SelectedBrush = CreateBrush(_selectedBrushName);
        }


        private LinearGradientBrushViewModel _linearGradientBrushProperties;
        public LinearGradientBrushViewModel LinearGradientBrushProperties
        {
            get { return _linearGradientBrushProperties; }
            set { SetProperty(ref _linearGradientBrushProperties, value); }
        }

        private bool _isLinearGradientBrushPropertiesVisible;
        public bool IsLinearGradientBrushPropertiesVisible
        {
            get { return _isLinearGradientBrushPropertiesVisible; }
            private set { SetProperty(ref _isLinearGradientBrushPropertiesVisible, value);}
        }

        private void LinearGradientBrushPropertiesOnOnLinearGradientBrushPropertiesChanged(object sender, EventArgs eventArgs)
        {
            this.SelectedBrush = CreateBrush(_selectedBrushName);
        }


        private RadialGradientBrushViewModel _radialGradientBrushProperties;

        public RadialGradientBrushViewModel RadialGradientBrushProperties
        {
            get { return _radialGradientBrushProperties; }
            set { SetProperty(ref _radialGradientBrushProperties, value); }
        }

        private bool _isRadialGradientBrushPropertiesVisible;

        public bool IsRadialGradientBrushPropertiesVisible
        {
            get { return _isRadialGradientBrushPropertiesVisible; }
            private set { SetProperty(ref _isRadialGradientBrushPropertiesVisible, value); }
        }
        private void RadialGradientBrushPropertiesOnOnRadialGradientBrushPropertiesChanged(object sender, EventArgs eventArgs)
        {
            this.SelectedBrush = CreateBrush(_selectedBrushName);
        }




        #endregion





        private void ToggleVisibility(string shapeName)
        {
            switch (shapeName)
            {
                case  "Rectangle":
                    this.RectangleIsVisible = true;
                    this.EllipseIsVisible = false;
                    break;
                case "Ellipse":
                    this.RectangleIsVisible = false;
                    this.EllipseIsVisible = true;
                    break;
                default:
                    this.RectangleIsVisible = true;
                    this.EllipseIsVisible = false;
                    break;
            }
        }

        private void ToggleBrushPropertiesVisibility(string brushName)
        {
            if (brushName == typeof(SolidColorBrush).Name)
            {
                this.IsSolidBrushPropertiesVisible = true;
                this.IsLinearGradientBrushPropertiesVisible = false;
                this.IsRadialGradientBrushPropertiesVisible = false;
            }
            if (brushName == typeof (LinearGradientBrush).Name)
            {
                this.IsSolidBrushPropertiesVisible = false;
                this.IsLinearGradientBrushPropertiesVisible = true;
                this.IsRadialGradientBrushPropertiesVisible = false;
            }
            if (brushName == typeof (RadialGradientBrush).Name)
            {
                this.IsSolidBrushPropertiesVisible = false;
                this.IsLinearGradientBrushPropertiesVisible = false;
                this.IsRadialGradientBrushPropertiesVisible = true;
            }
        }


        private Brush CreateBrush(string brushName)
        {
            /*
             * Done: implement a Factory or builder type class to abstract the creation
             * of the specific Brushes, which all take different parameters.  
             * Considerations are the number of varioables that must be passed to the
             * factory / builder class needed to instantiate the class.  For each type of 
             * class being instantiated, a different number and type of parameters are 
             * needed and and this goes against the intention of the AbstractFactory pattern.  
             * It is more appropriate for a Builder pattern.  A fluent style of implementing
             * the Builder pattern is very neat in this instance. so I ended up using a 
             * fluid style of Builder pattern implmentation following the example
             * in this article by Stefano Ricciardi:
             * http://stefanoricciardi.com/2010/04/14/a-fluent-builder-in-c/
             * 
             * The following article by Jose Luis Ordiales (Java based) was also helpful:
             * https://jlordiales.me/2012/12/13/the-builder-pattern-in-practice/
             * 
             * Using this builder class allows only those properties needed by the 
             * particular class of brush to be defined.  All other properties can 
             * be left undefined, but the class returned is always in a consistent
             * state.  The builder creates an instance of the MyBrush class which
             * has a Brush property that gets the instance created by the builder.
             * 
             * Further consideration: It would be nice to abstract the fluid logic
             * of this method into an abstract factory or even simply a helper class
             * but that the re-introduces the problems of the numbers and diversity
             * of parameters needed to class instantiation, so this method is kept
             * within the View|Model class: The creation of the brush is entirely
             * a UI concern so it is not inappropriate to keep this logic here, but
             * as a private method with a single responsibility, therefore adhering
             * to the SRP of OO design.  (Comments welcome).
             * 
             * The overriding concern is that each brush that is instantiated, MUST
             * be instantiated with the current values obtained from the UI.  If the
             * brushes were always created using a different source of values, it 
             * would be entirely poissible and appropriate to abstract this completely
             * from this ViewModel.
             * 
             * Note: Original code and comments can be found in UsingBrushes project.
             */

            //  Define the Builder instance
            IMyBrushBuilder bldr = new MyBrushBuilder(brushName);

            //  Set the properties and return a "SolidColorBrush".
            if (brushName == typeof (SolidColorBrush).Name)
            {
                MyBrush myBrush = bldr.Colour(_solidBrushProperties.SelectedColor);  //.Build();
                return myBrush.Brush;
            }

            //  Set the properties and return a "LinearGradientBrush".
            if (brushName == typeof (LinearGradientBrush).Name)
            {
                /*
                 * Unlike the example above, using the Build() method instead of the in-built
                 * conversion, removes the need to explicitly reference the concrete "MyBrush" 
                 * type and use the "var" keyword instead, or a Return statement directly.
                 */
                var myBrush = bldr.StartPoint(_linearGradientBrushProperties.StartPoint)
                    .EndPoint(_linearGradientBrushProperties.EndPoint)
                    .GradientStops(_linearGradientBrushProperties.GradientStops.ToGradientStopCollection())
                    .Build();
                return myBrush.Brush;
            }

            //  Set the properties and return a "RadialGradientBrush".
            if (brushName == typeof(RadialGradientBrush).Name)
            {
                /*
                 * To further abstract the concrete type could be achieved by having the class
                 * implement in interface, such as IMyBrush, but not doing that here as this 
                 * is not the point
                 */
                IMyBrush myBrush = bldr.RadiusX(_radialGradientBrushProperties.RadiusX)
                    .RadiusY(_radialGradientBrushProperties.RadiusY)
                    .GradientOrigin(_radialGradientBrushProperties.GradientOrigin)
                    .Centre(_radialGradientBrushProperties.Centre)
                    .GradientStops(_radialGradientBrushProperties.GradientStops.ToGradientStopCollection())
                    .Build();
                return myBrush.Brush;
            }

            return new SolidColorBrush(Colors.Blue);

        }
    }
}
