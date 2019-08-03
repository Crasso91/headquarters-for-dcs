namespace Headquarters4DCS
{
    public enum FeatureFlag
    {
        /// <summary>
        /// If no spawn point is found for this feature, abort mission generation instead of just printing a warning in the log. Always enabled for features belonging to the "Objective" category.
        /// </summary>
        Required,

        Unique,
        UniqueOfType
    }
}
