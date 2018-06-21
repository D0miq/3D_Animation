namespace Registration_v2.UI
{
    /// <summary>
    /// The <see cref="IShowable"/> interface says which panel can be shown.
    /// </summary>
    public interface IShowable
    {
        /// <summary>
        /// Gets or sets a value indicating whether a panel is shown.
        /// </summary>
        bool IsShown { get; set; }
    }
}
