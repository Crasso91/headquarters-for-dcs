namespace Headquarters4DCS.Forms
{
    public enum TheaterUpdateType
    {
        /// <summary>
        /// Update the whole theater. Only when theater has changed.
        /// </summary>
        Full,
        /// <summary>
        /// Update all locations.
        /// </summary>
        AllLocations,
        /// <summary>
        /// Update only the selected location.
        /// </summary>
        SelectedLocation
    }
}
