using Copernicus.Core.Views;

namespace Copernicus.Modules.SecurityMaster.Securities
{
    public sealed class SecurityViewModel : BaseViewModel
    {
        public SecurityViewModel() { }

        public SecurityViewModel(
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
            SecurityName = securityName;
            IssueDate = issueDate;
            Ticker = ticker;
            Cusip = cusip;
            Isin = isin;
            Sedol = sedol;
            LastPrice = lastPrice;
            IsPrivate = isPrivate;
        }

        /*
         * TODO: Make these proper objects!
         */
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
