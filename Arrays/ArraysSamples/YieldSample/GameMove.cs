using System.Collections;
using static System.Console;

namespace Wrox.ProCSharp.Arrays
{
    public class GameMoves
    {
        private IEnumerator _cross;
        private IEnumerator _circle;

        public GameMoves()
        {
            _cross = Cross();
            _circle = Circle();
        }

        private int _move = 0;
        const int MaxMoves = 9;

        public IEnumerator Cross()
        {
            while (true)
            {
                WriteLine($"Cross, move {_move}");
                if (++_move >= MaxMoves)
                    yield break;
                yield return _circle;
            }
        }

        public IEnumerator Circle()
        {
            while (true)
            {
                WriteLine("Circle, move {0}", _move);
                if (++_move >= MaxMoves)
                    yield break;
                yield return _cross;
            }
        }
    }

}
