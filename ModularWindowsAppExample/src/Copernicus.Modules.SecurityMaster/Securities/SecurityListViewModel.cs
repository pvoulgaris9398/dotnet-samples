using Copernicus.Core.Views;

namespace Copernicus.Modules.SecurityMaster.Securities
{
    internal class SecurityListViewModel : BaseListViewModel<SecurityViewModel>
    {
        public override Func<
            BaseListViewModel<SecurityViewModel>,
            IEnumerable<SecurityViewModel>
        > ItemsFactory =>
            lvm =>
                [
                    new SecurityViewModel(
                        "Security 1",
                        DateTime.Now,
                        "ticker1",
                        "cusip1",
                        "isin1",
                        "sedol1",
                        101m,
                        true
                    ),
                    new SecurityViewModel(
                        "Security 2",
                        DateTime.Now,
                        "ticker2",
                        "cusip2",
                        "isin2",
                        "sedol2",
                        202,
                        true
                    ),
                    new SecurityViewModel(
                        "Security 3",
                        DateTime.Now,
                        "ticker3",
                        "cusip3",
                        "isin3",
                        "sedol3",
                        303,
                        true
                    ),
                    new SecurityViewModel(
                        "Security 4",
                        DateTime.Now,
                        "ticker4",
                        "cusip4",
                        "isin4",
                        "sedol4",
                        45.56m,
                        true
                    ),
                    new SecurityViewModel(
                        "Security 5",
                        DateTime.Now,
                        "ticker5",
                        "cusip5",
                        "isin5",
                        "sedol5",
                        22.758m,
                        false
                    ),
                    new SecurityViewModel(
                        "Security 6",
                        DateTime.Now,
                        "ticker6",
                        "cusip6",
                        "isin6",
                        "sedol6",
                        44m,
                        true
                    ),
                    new SecurityViewModel(
                        "Security 7",
                        DateTime.Now,
                        "ticker7",
                        "cusip7",
                        "isin7",
                        "sedol7",
                        55.7584m,
                        true
                    ),
                    new SecurityViewModel(
                        "Security 8",
                        DateTime.Now,
                        "ticker8",
                        "cusip8",
                        "isin8",
                        "sedol8",
                        6.987254m,
                        false
                    ),
                    new SecurityViewModel(
                        "Security 9",
                        DateTime.Now,
                        "ticker9",
                        "cusip9",
                        "isin9",
                        "sedol9",
                        7845m,
                        false
                    ),
                ];

        public override Func<BaseListViewModel<SecurityViewModel>, SecurityViewModel> ItemFactory =>
            lvm => new SecurityViewModel(
                $"Security {Count + 1}",
                new DateTime(2022, 3, 12),
                $"ticker {Count + 1}",
                $"cusip {Count + 1}",
                $"isin {Count + 1}",
                $"sedol {Count + 1}",
                202m,
                true
            );
    }
}
