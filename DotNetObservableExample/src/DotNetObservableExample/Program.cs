/*
Build array of Test Options
Test options can display there own menu
Show menu of options
React to user input and call appropriate option
*/

(Func<string>, Action)[] tasks =
[
    (ExpressionExample.Description, ExpressionExample.Execute),
    (DemonstrateObservable.Description, DemonstrateObservable.Execute),
    (BasicObservableExample.Description, BasicObservableExample.Execute),
    (MoneyExample.Description, MoneyExample.Execute),
    (MicrosoftBaggageHandleExample.Description, MicrosoftBaggageHandleExample.Execute),
    (
        IntegrationWithFSharpAssemblyExample.Description,
        IntegrationWithFSharpAssemblyExample.Execute
    ),
];

while (Tasks.ShowMenu(tasks))
{
    var line = Console.ReadLine() ?? "";

    if (line == "Q")
        break;

    if (line == "C")
        Console.Clear();

    if (
        int.TryParse(line, out var index)
        && tasks.Length > --index
        && index >= 0
        && tasks[index] is (Func<string>, Action) task
    )
    {
        Console.WriteLine("Executing the '{0}'...", task.Item1());
        task.Item2();
        Console.WriteLine("'{0}' complete.", task.Item1());
    }
}
