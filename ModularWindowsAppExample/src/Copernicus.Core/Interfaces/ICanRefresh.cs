namespace Copernicus.Core.Interfaces
{
    public interface ICanRefresh
    {
        /// <summary>
        /// Action allowed from a business
        /// or logical point-of-view
        /// </summary>
        /// <returns>True if action allowed, false otherwise</returns>
        bool CanRefresh { get; }

        /// <summary>
        /// Action allowed from a
        /// permission point-of-view
        /// </summary>
        /// <returns>True if action allowed, false otherwise</returns>
        bool MayRefresh { get; }

        /// <summary>
        /// Factory method to create objects of type T
        /// </summary>
        void Refresh();
    }
}
