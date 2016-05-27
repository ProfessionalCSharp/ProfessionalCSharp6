namespace DisposableSample
{
    public class Program
    {
        public static void Main()
        {
            using (var resource = new SomeResource())
            {
                resource.Foo();
            }
        }
    }
}
