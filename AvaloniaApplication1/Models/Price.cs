using System;

namespace AvaloniaApplication1.Models
{
    public record Price(
        string Security,
        string Currency,
        DateTime Timestamp,
        decimal Value)
    {
    }
}
