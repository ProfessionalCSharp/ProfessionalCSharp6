
namespace Wrox.ProCSharp.Generics
{
  public class LinkedListNode<T>
  {
    public LinkedListNode(T value)
    {
      Value = value;
    }

    public T Value { get; private set; }
    public LinkedListNode<T> Next { get; internal set; }
    public LinkedListNode<T> Prev { get; internal set; }
  }
}
