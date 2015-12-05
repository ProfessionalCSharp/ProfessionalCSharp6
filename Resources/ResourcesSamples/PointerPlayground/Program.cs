using static System.Console;

namespace PointerPlayground
{
    public class Program
    {
        unsafe public static void Main(string[] args)
        {
            int x = 10;
            short y = -1;
            byte y2 = 4;
            double z = 1.5;
            int* pX = &x;
            short* pY = &y;
            double* pZ = &z;

            WriteLine($"Address of x is 0x{(ulong)&x:X}, size is {sizeof(int)}, value is {x}");
            WriteLine($"Address of y is 0x{(ulong)&y:X}, size is {sizeof(short)}, value is {y}");
            WriteLine($"Address of y2 is 0x{(ulong)&y2:X}, size is {sizeof(byte)}, value is {y2}");
            WriteLine($"Address of z is 0x{(ulong)&z:X}, size is {sizeof(double)}, value is {z}");
            WriteLine($"Address of pX=&x is 0x{(ulong)&pX:X}, size is {sizeof(int*)}, value is 0x{(ulong)pX:X}");
            WriteLine($"Address of pY=&y is 0x{(ulong)&pY:X}, size is {sizeof(short*)}, value is 0x{(ulong)pY:X}");
            WriteLine($"Address of pZ=&z is 0x{(ulong) &pZ:X}, size is {sizeof(double*)}, value is 0x{(ulong)pZ:X}");
            *pX = 20;
            WriteLine($"After setting *pX, x = {x}");
            WriteLine($"*pX = {*pX}");

            pZ = (double*)pX;
            WriteLine($"x treated as a double = {*pZ}");

            ReadLine();

        }
    }
}
