using System.Reflection;
using System.Runtime.Loader;

namespace Copernicus.Core.Modules
{
    public class CopernicusAssemblyLoadContext(string mainAssemblyToLoadPath) : AssemblyLoadContext(isCollectible: true)
    {
        //private readonly AssemblyDependencyResolver _resolver = new(mainAssemblyToLoadPath);

        protected override Assembly? Load(AssemblyName name)
        {
            /*  See: https://learn.microsoft.com/en-us/dotnet/core/dependency-loading/understanding-assemblyloadcontext
             */
            return null;
            /*
             * string? assemblyPath = _resolver.ResolveAssemblyToPath(name);
             * return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
             */
        }
    }
}
