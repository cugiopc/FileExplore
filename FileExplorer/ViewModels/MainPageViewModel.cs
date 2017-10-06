namespace FileExplorer.ViewModels
{
    using Contract;
    using Caliburn.Micro;
    using PropertyChanged;
    using System.ComponentModel.Composition;
    using ILog = log4net.ILog;
    using LogManager = log4net.LogManager;

    /// <summary>
    /// Class MainPageViewModel.
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    [Export]
    [AddINotifyPropertyChangedInterface]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainPageViewModel : Screen
    {
        #region Varialbes

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainPageViewModel));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public HeaderViewModel Header { get; set; } = IoC.Get<HeaderViewModel>();

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public BodyViewModel Body { get; set; } = IoC.Get<BodyViewModel>();

        #endregion

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel" /> class.
        /// </summary>
        public MainPageViewModel()
        {
            Logger.Debug("MainPageViewModel...");
            this.Header.SearchSetting = new FilterSetting
            {
                ProjectFilter = "*.csproj",
                NugetFilter = "*.nuspec",
                ReferenceAssemblyFilter = "",
                RootDir = ""
            };
            this.Header.ProjectMgr();
            Logger.Debug("MainPageViewModel...DONE");
        }

        #endregion
    }
}
