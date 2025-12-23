namespace Copernicus.Core.UnitTest;

public class UnitTest1 : BaseTest<UnitTest1>
{
    [Fact]
    public void Test1()
    {
        var temp = OnArrange(Array.Empty<string>);
        var actual = OnAct(Array.Empty<int>);
    }
}
