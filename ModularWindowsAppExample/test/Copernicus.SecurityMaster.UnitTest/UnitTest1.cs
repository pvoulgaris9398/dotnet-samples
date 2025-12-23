using Copernicus.Core.UnitTest;
using Copernicus.Modules.SecurityMaster;

namespace Copernicus.SecurityMaster.UnitTest;

public class UnitTest1 : BaseTest<MainLayoutViewModel>
{
    [Fact]
    public void Test1()
    {
        var temp = OnArrange(Array.Empty<string>);
        var actual = OnAct(Array.Empty<int>);
    }
}
