using UsingMoreBrushes.Models;

namespace UsingMoreBrushes.Interfaces.Factories
{
    public interface IColourInfoFactory
    {
        ColourInfo Create();
        ColourInfo Create(string colourName);
    }
}
