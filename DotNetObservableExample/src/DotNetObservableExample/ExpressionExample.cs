using System.Linq.Expressions;

namespace DotNetObservableExample
{
    internal class ExpressionExample
    {
        public static string Description() => "Demonstrate Expression Trees";

        public static void Execute()
        {
            Console.WriteLine();

            var p0 = Expression.Parameter(typeof(string), "p0");
            var p1 = Expression.Parameter(typeof(object), "p1");

            MethodCallExpression methodCall = Expression.Call(
                typeof(Console).GetMethod("WriteLine", [typeof(string), typeof(object)]),
                p0,
                p1
            );

            Expression
                .Lambda<Action<string, object>>(methodCall, [p0, p1])
                .Compile()("Number was: '{0}'", 662);
        }
    }
}
