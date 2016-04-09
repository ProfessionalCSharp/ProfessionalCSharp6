namespace PatternMatchingSample
{
    // sample code from https://github.com/dotnet/roslyn/blob/features/patterns/docs/features/patterns.md changed from records to classes
    // because records are not yet available with C# 7
    abstract class Expr { }
    class X : Expr { }
    class Const : Expr
    {
        public Const(double value)
        {

        }
    }
    class Add : Expr
    {
        public Add(Expr left, Expr right)
        {

        }
    }
    class Mult : Expr
    {
        public Mult(Expr left, Expr right)
        {

        }
    }
    class Neg : Expr
    {
        public Neg(Expr value)
        {

        }
    }
}
