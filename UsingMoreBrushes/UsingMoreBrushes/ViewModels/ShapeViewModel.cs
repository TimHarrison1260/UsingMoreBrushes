namespace UsingMoreBrushes.ViewModels
{
    public class ShapeViewModel: BindableViewModelBase
    {

        public ShapeViewModel(string name)
        {
            this.Name = name;
        }


        public string Name { get; private set; }



    }
}
