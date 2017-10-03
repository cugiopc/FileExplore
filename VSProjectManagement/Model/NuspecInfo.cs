namespace VSProjectManagement.Model
{
    using System.IO;
    using FileExplorer.Contract.Model;
    using Helper;

    /// <summary>
    /// Class NuspecInfo.
    /// </summary>
    /// <seealso cref="FileExplorer.Contract.Model.ItemInfo" />
    public class NuspecInfo : ItemInfo
    {
        /// <summary>
        /// Gets or sets the nuget version.
        /// </summary>
        /// <value>The nuget version.</value>
        public string NugetVersion { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NuspecInfo"/> class.
        /// </summary>
        public NuspecInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NuspecInfo"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public NuspecInfo(FileInfo data) : this ()
        {
            if (data == null)
            {
                return;
            }

            this.Name = System.IO.Path.GetFileName(data.Name);
            this.Path = data.FullName;

            this.BuildNugetVersion();
        }
    }
}
