using System.Reflection;
using System.Runtime.Loader;

namespace Copernicus.Core.Modules
{
    public class CopernicusAssemblyLoadContext(string mainAssemblyToLoadPath) : AssemblyLoadContext(isCollectible: true)
    {
        private readonly AssemblyDependencyResolver _resolver = new(mainAssemblyToLoadPath);

        /*
        public Assembly Load(string name)
        {
            var assembly = Load(new AssemblyName() { Name = name });
            //TODO: Implement better exception class
            return assembly is null ? throw new Exception($"Unable to load {name}!") : assembly;
        }
        */
        protected override Assembly? Load(AssemblyName name)
        {
            string? assemblyPath = _resolver.ResolveAssemblyToPath(name);
            return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
        }
    }
}
