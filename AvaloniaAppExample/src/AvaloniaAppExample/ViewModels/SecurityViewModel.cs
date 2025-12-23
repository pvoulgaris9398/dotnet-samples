using AvaloniaAppExample.Models;

namespace AvaloniaAppExample.ViewModels
{
    public class SecurityViewModel(Security item)
    {
        protected Security Item { get; } = item;

        public long Id => Item.Id;
        public string SecurityName => Item.SecurityName;
        public DateTime IssueDate => Item.IssueDate;
        public string Ticker => Item.Ticker;
        public string Cusip => Item.Cusip;
        public string Isin => Item.Isin;
        public string Sedol => Item.Sedol;
        public decimal LastPrice => Item.LastPrice;
        public bool IsPrivate => Item.IsPrivate;
    }
}
