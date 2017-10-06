namespace FileExplorer.ViewModels
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows;
    using Caliburn.Micro;
    using Contract;
    using Interface;
    using PropertyChanged;
    using Validation;
    using ILog = log4net.ILog;
    using LogManager = log4net.LogManager;

    /// <summary>
    /// Class HeaderViewModel.
    /// </summary>
    /// <seealso cref="FileExplorer.Interface.IHeaderBase" />
    [Export]
    [Export(typeof(IHeaderBase))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [AddINotifyPropertyChangedInterface]
    public class HeaderViewModel : IHeaderBase
    {
        #region Variables

        /// <summary>
        /// Gets the content management.
        /// </summary>
        /// <value>The content management.</value>
        private IContentManagement contentManagement => IoC.Get<IContentManagement>();

        /// <summary>
        /// The filter text
        /// </summary>
        private string filterText;

        /// <summary>
        /// The logger  
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(HeaderViewModel));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the root dir.
        /// </summary>
        /// <value>The root dir.</value>
        [AlsoNotifyFor("HasText")]
        [StringRequireValidator("Filter pattern should be not empty")]
        public string FilterText
        {
            get { return this.filterText; }
            set
            {
                this.filterText = value;
                this.SearchTextChangedEvent?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Gets the has text.
        /// </summary>
        /// <value>The has text.</value>
        public Visibility HasText => string.IsNullOrWhiteSpace(this.FilterText) ? Visibility.Collapsed : Visibility.Visible;

        /// <summary>
        /// Gets or sets the search setting.
        /// </summary>
        /// <value>The search setting.</value>
        public FilterSetting SearchSetting { get; set; }

        /// <summary>
        /// Occurs when [search text changed event].
        /// </summary>
        public event EventHandler<string> SearchTextChangedEvent;

        #endregion

        #region Commands handling

        /// <summary>
        /// Clears the text.
        /// </summary>
        public void ClearText()
        {
            this.FilterText = string.Empty;
        }

        /// <summary>
        /// Projects the MGR.
        /// </summary>
        public void ProjectMgr()
        {
            Logger.Debug("ProjectMgr... - Show ProjectMgrViewModel");
            this.contentManagement.ShowContent(IoC.Get<ProjectMgrViewModel>());
            Logger.Debug("ProjectMgr... - Show ProjectMgrViewModel..DONE");
        }

        /// <summary>
        /// Nugets the MGR.
        /// </summary>
        public void NugetMgr()
        {
            Logger.Debug("NugetMgr... - Show NugetMgrViewModel");
            this.contentManagement.ShowContent(IoC.Get<NugetMgrViewModel>());
            Logger.Debug("NugetMgr... - Show NugetMgrViewModel..DONE");
        }
        /// <summary>
        /// References the ass MGR.
        /// </summary>
        public void ReferenceAssMgr()
        {
            Logger.Debug("ReferenceAssMgr... - Show ReferenceAssMgrViewModel");
            this.contentManagement.ShowContent(IoC.Get<ReferenceAssMgrViewModel>());
            Logger.Debug("ReferenceAssMgr... - Show ReferenceAssMgrViewModel..DONE");
        }

        /// <summary>
        /// Exits this instance.
        /// </summary>
        public void Exit()
        {
            Logger.Debug("Exit...");
            Application.Current.MainWindow.Close();
            Logger.Debug("Exit...DONE");
        }

        #endregion


    }
}
