namespace Copernicus.Core.Modules
{
    public record ModuleInstance(
        WeakReference WeakReference,
        CopernicusAssemblyLoadContext AssemblyLoadContext
    );
}
