namespace FileExplorer.Contract
{
    /// <summary>
    /// Class FilterSetting.
    /// </summary>
    public class FilterSetting
    {
        /// <summary>
        /// Gets or sets the root dir.
        /// </summary>
        /// <value>The root dir.</value>
        public string RootDir { get; set; }

        /// <summary>
        /// Gets or sets the project filter.
        /// </summary>
        /// <value>The project filter.</value>
        public string ProjectFilter { get; set; }

        /// <summary>
        /// Gets or sets the nuget filter.
        /// </summary>
        /// <value>The nuget filter.</value>
        public string NugetFilter { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public object[] Parameter { get; set; }

        /// <summary>
        /// Gets or sets the reference assembly filter.
        /// </summary>
        /// <value>The reference assembly filter.</value>
        public string ReferenceAssemblyFilter { get; set; }
    }
}
