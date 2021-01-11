namespace Taco.Core.Configuration.Models
{
    /// <summary>
    ///     A data base settings.
    /// </summary>
    public class DataBaseSettings
    {
        /// <summary>
        ///     Gets or sets the name of the connection string.
        /// </summary>
        ///
        /// <value>
        ///     The name of the connection string.
        /// </value>
        public string ConnectionStringName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the data source.
        /// </summary>
        ///
        /// <value>
        ///     The name of the data source.
        /// </value>
        public string DataSource { get; set; }
    }
}