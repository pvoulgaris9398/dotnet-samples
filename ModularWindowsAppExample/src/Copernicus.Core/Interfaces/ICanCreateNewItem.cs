using Copernicus.Core.Views;

namespace Copernicus.Core.Interfaces
{
    public interface ICanCreateNewItem
    {
        /// <summary>
        /// Action allowed from a business
        /// or logical point-of-view
        /// </summary>
        /// <returns>True if action allowed, false otherwise</returns>
        bool CanCreate { get; }

        /// <summary>
        /// Action allowed from a
        /// permission point-of-view
        /// </summary>
        /// <returns>True if action allowed, false otherwise</returns>
        bool MayCreate { get; }

        /// <summary>
        /// Factory method to create objects of type T
        /// </summary>
        /// <returns>Returns a fully-create object of type T</returns>
        BaseViewModel Create();
    }
}
