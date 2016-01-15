using DataLib;
using System;
using System.Linq.Expressions;
using static System.Console;

namespace ExpressionTreeSample
{
    class Program
    {
        static void Main()
        {
            Expression<Func<Racer, bool>> expression = r => r.Country == "Brazil" && r.Wins > 6;

            DisplayTree(0, "Lambda", expression);
        }

        private static void DisplayTree(int indent, string message, Expression expression)
        {
            string output = $"{ string.Empty.PadLeft(indent, '>')} {message} ! NodeType: {expression.NodeType}; Expr: {expression}";

            indent++;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    WriteLine(output);
                    LambdaExpression lambdaExpr = (LambdaExpression)expression;
                    foreach (var parameter in lambdaExpr.Parameters)
                    {
                        DisplayTree(indent, "Parameter", parameter);
                    }
                    DisplayTree(indent, "Body", lambdaExpr.Body);
                    break;
                case ExpressionType.Constant:
                    ConstantExpression constExpr = (ConstantExpression)expression;
                    WriteLine($"{output} Const Value: {constExpr.Value}");
                    break;
                case ExpressionType.Parameter:
                    ParameterExpression paramExpr = (ParameterExpression)expression;
                    WriteLine($"{output} Param Type: {paramExpr.Type.Name}");
                    break;
                case ExpressionType.Equal:
                case ExpressionType.AndAlso:
                case ExpressionType.GreaterThan:
                    BinaryExpression binExpr = (BinaryExpression)expression;
                    if (binExpr.Method != null)
                    {
                        WriteLine($"{output} Method: {binExpr.Method.Name}");
                    }
                    else
                    {
                        WriteLine(output);
                    }
                    DisplayTree(indent, "Left", binExpr.Left);
                    DisplayTree(indent, "Right", binExpr.Right);
                    break;
                case ExpressionType.MemberAccess:
                    MemberExpression memberExpr = (MemberExpression)expression;
                    WriteLine($"{output} Member Name: {memberExpr.Member.Name}, Type: {memberExpr.Type.Name}");
                    DisplayTree(indent, "Member Expr", memberExpr.Expression);
                    break;
                default:
                    WriteLine();
                    WriteLine($"{expression.NodeType} {expression.Type.Name}");
                    break;
            }
        }
    }
}
