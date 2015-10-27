using static System.Console;

namespace Wrox.ProCSharp.Generics
{
  public class ShapeDisplay : IDisplay<Shape>
  {
    public void Show(Shape s) => WriteLine($"{s.GetType().Name} Width: {s.Width}, Height: {s.Height}");

  }
}
