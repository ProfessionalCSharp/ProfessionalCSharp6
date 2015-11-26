using static System.Console;

namespace ExceptionFilters
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ThrowWithErrorCode(405);

            }
            catch (MyCustomException ex) when (ex.ErrorCode == 405)
            {
                WriteLine($"Exception caught with filter {ex.Message} and {ex.ErrorCode}");
            }
            catch (MyCustomException ex)
            {
                WriteLine($"Exception caught {ex.Message} and {ex.ErrorCode}");
            }

            ReadLine();
        }

        public static void ThrowWithErrorCode(int code)
        {
            throw new MyCustomException("Error in Foo") { ErrorCode = code };
        }
    }

}
