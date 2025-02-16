using AvaloniaApplication1.Models;
using System;

namespace AvaloniaApplication1.ViewModels
{
    public class SecurityViewModel
    {

        public SecurityViewModel(Security item)
        {
            Item = item;
        }

        protected Security Item { get; }

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
