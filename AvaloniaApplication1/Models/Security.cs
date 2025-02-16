using System;

namespace AvaloniaApplication1.Models
{
    public class Security
    {
        public static Security Create(long id) => new Security(
            id,
            $"Security Name # {id}",
            DateTime.UtcNow,
            $"Ticker # {id}",
            $"Cusip # {id}",
            $"Isin # {id}",
            $"Sedol # {id}",
            101.55m,
            true);

        public Security(
            long id,
            string securityName,
            DateTime issueDate,
            string ticker,
            string cusip,
            string isin,
            string sedol,
            decimal lastPrice,
            bool isPrivate)
        {
            Id = id;
            SecurityName = securityName;
            IssueDate = issueDate;
            Ticker = ticker;
            Cusip = cusip;
            Isin = isin;
            Sedol = sedol;
            LastPrice = lastPrice;
            IsPrivate = isPrivate;
        }
        public long Id { get; }
        public string SecurityName { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Cusip { get; set; } = string.Empty;
        public string Isin { get; set; } = string.Empty;
        public string Sedol { get; set; } = string.Empty;
        public decimal LastPrice { get; set; }
        public bool IsPrivate { get; set; }
    }
}
