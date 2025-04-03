using System;
using System.Reactive;
using ReactiveUI;

/*
 * Good question and answer:
 * https://stackoverflow.com/questions/78687228/why-use-whenanyvalue-instead-of-raisepropertychanged
 * 
 * https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/MVVM/BasicMvvmSample#step-2-add-properties-to-our-viewmodel-1
 * 
 * https://habr.com/en/articles/454074/
 * 
 * How to handle (possibly) multiple properties in:
 * WhenAnyValue or WhenAny???
 * 
 */

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

            IObservable<bool> foo = this.WhenAny(
                x => x.DefaultText


                , x => true);

            SubmitCommand = ReactiveCommand.Create(() =>
            {
                DefaultText = "Clicked";
            }, isInputValid);
        }

        public DateTime CurrentDate => DateTime.Now;

        public string Display { get; } = "Sandbox Area";

        public string DefaultText
        {
            get => _defaultText;
            set => this.RaiseAndSetIfChanged(ref _defaultText, value);
        }

        public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    }
}
