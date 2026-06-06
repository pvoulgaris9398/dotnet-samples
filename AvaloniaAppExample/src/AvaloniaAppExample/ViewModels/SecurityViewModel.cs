using AvaloniaAppExample.Models;

namespace AvaloniaAppExample.ViewModels
{
    public sealed class SecurityViewModel(Security item)
    {
#pragma warning disable CS0628 // New protected member declared in sealed type
        protected Security Item { get; } = item;
#pragma warning restore CS0628 // New protected member declared in sealed type

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
