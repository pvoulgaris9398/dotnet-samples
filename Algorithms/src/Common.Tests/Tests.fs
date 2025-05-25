namespace Common

module Tests =

    open Xunit

    (*
    https://developercommunity.visualstudio.com/t/Test-discovery-aborted-Could-not-find-t/10512939?sort=newest
    -For projects that reference a test framework but don't reference Microsoft.NET.Test.Sdk
    - Usually done for projects holding common test functionality
    - Add the following to that project file

    <ItemGroup>
        <ProjectCapability Remove="TestContainer" />
    </ItemGroup

    *)

    [<Fact>]
    let ``Passing Test`` () = Assert.True(true)

    [<Fact>]
    let ``Failing Test`` () = Assert.True(false)
