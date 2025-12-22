using System;

namespace AvaloniaAppExample.Models
{
    public class Security(
        long id,
        string securityName,
        DateTime issueDate,
        string ticker,
        string cusip,
        string isin,
        string sedol,
        decimal lastPrice,
        bool isPrivate
    )
    {
        public static Security Create(long id) =>
            new(
                id,
                $"Security Name # {id}",
                DateTime.UtcNow,
                $"Ticker # {id}",
                $"Cusip # {id}",
                $"Isin # {id}",
                $"Sedol # {id}",
                101.55m,
                true
            );

        public long Id { get; } = id;
        public string SecurityName { get; set; } = securityName;
        public DateTime IssueDate { get; set; } = issueDate;
        public string Ticker { get; set; } = ticker;
        public string Cusip { get; set; } = cusip;
        public string Isin { get; set; } = isin;
        public string Sedol { get; set; } = sedol;
        public decimal LastPrice { get; set; } = lastPrice;
        public bool IsPrivate { get; set; } = isPrivate;
    }
}
