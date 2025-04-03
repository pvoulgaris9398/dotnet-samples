using System;
using System.Reactive;
using ReactiveUI;

namespace AvaloniaAppExample.ViewModels
{
    public class SandboxViewModel : ViewModelBase
    {
        private string _defaultText = "Enter...";

        public SandboxViewModel()
        {

            IObservable<bool> isInputValid = this.WhenAnyValue(
                x => x.DefaultText,
                x => !string.IsNullOrWhiteSpace(x) && x.Length > 7
                );

            SubmitCommand = ReactiveCommand.Create(() =>
            {
                DefaultText = "Clicked";
            }, isInputValid);
        }
        public string Display { get; } = "Sandbox Area";

        public string DefaultText
        {
            get => _defaultText;
            set => this.RaiseAndSetIfChanged(ref _defaultText, value);
        }

        public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    }
}
