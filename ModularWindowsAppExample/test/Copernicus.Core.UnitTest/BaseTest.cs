namespace Copernicus.Core.UnitTest
{
    public class BaseTest<T>
        where T : class
    {
        protected void OnArrange(Action? action = null)
        {
            action?.Invoke();
        }

        protected V OnArrange<V>(Func<V> action)
        {
            return action.Invoke();
        }

        protected void OnAct(Action action)
        {
            action.Invoke();
        }

        protected V OnAct<V>(Func<V> action)
        {
            return action.Invoke();
        }

        protected void OnAssert(Action? action = null)
        {
            action?.Invoke();
        }
    }
}
