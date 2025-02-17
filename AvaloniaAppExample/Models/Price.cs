using System;

namespace AvaloniaAppExample.Models
{
    public record Price(
        string Security,
        string Currency,
        DateTime Timestamp,
        decimal Value)
    {
    }
}
