using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaAppExample.ViewModels;
using AvaloniaAppExample.Views;

namespace AvaloniaAppExample
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            if (param is null)
                return null;

            var name = param
                .GetType()
                .FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            return type != null
                ? (Control)Activator.CreateInstance(type)!
                : new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data) => data is ViewModelBase;
    }
}
