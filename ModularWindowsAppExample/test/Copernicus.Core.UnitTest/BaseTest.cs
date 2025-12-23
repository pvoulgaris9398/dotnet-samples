namespace Copernicus.Core.UnitTest
{
    public class BaseTest<T>
        where T : class
    {
        protected void OnArrange(Action? action = null) => action?.Invoke();

        protected TV OnArrange<TV>(Func<TV> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            return action.Invoke();
        }

        protected void OnAct(Action action) => action?.Invoke();

        protected TV OnAct<TV>(Func<TV> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            return action.Invoke();
        }

        protected void OnAssert(Action? action = null) => action?.Invoke();
    }
}
