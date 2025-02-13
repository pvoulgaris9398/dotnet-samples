var result = GetValue(67);

LogMessage(result);

LogMethod.Set(msg =>
{
    var currentColor = ForegroundColor;
    try
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(msg);
    }
    finally
    {
        ForegroundColor = currentColor;
    }
});

result = GetValue(145);

LogMessage(result);

LogMethod.Set(msg =>
{
    var currentColor = ForegroundColor;
    try
    {
        ForegroundColor = ConsoleColor.Blue;
        WriteLine(msg);
    }
    finally
    {
        ForegroundColor = currentColor;
    }
});

result = GetValue(1066);
LogMessage(result);

LogMethod.Reset();

result = GetValue(5);

LogMessage(result);