namespace Copernicus.Core.Modules
{
    public record ModuleDefinition(
        string ModuleName,
        string ModuleClassName,
        string ModuleVersion = "1.0.0.0"
        )
    {
        public string TypeName => $"{ModuleClassName}, {ModuleName}";
        public string FileName => $"{ModuleName}.dll";
    }
}
