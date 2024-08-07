using System.Reflection;
using System.Runtime.Loader;

namespace Copernicus.Core.Modules
{
    public class CopernicusAssemblyLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        public CopernicusAssemblyLoadContext(string mainAssemblyToLoadPath) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(mainAssemblyToLoadPath);
        }

        public Assembly Load(string name)
        {
            var assemblyName = new AssemblyName() { Name = name };
            var assembly = Assembly.Load(assemblyName);
            //TODO: Implement better exception class
            if (assembly is null) { throw new Exception($"Unable to load {name}!"); }
            return assembly;
        }

        protected override Assembly? Load(AssemblyName name)
        {
            string? assemblyPath = _resolver.ResolveAssemblyToPath(name);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }
    }
}
