namespace FileExplorer.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using Model;
    using PropertyChanged;
    using VSProjectManagement.Controller;
    using System.Linq;
    using Validation;
    using VSProjectManagement.Model;
    using ILog = log4net.ILog;
    using LogManager = log4net.LogManager;

    /// <summary>
    /// Class NugetMgrViewModel.
    /// </summary>
    [Export]
    [AddINotifyPropertyChangedInterface]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NugetMgrViewModel : ContentBase<NugetMgrViewModel, Nuget>
    {
        #region Variables

        /// <summary>
        /// The project controller
        /// </summary>
        private readonly NugetController nugetController = IoC.Get<NugetController>();

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(NugetMgrViewModel));

        /// <summary>
        /// Gets or sets the root dir.
        /// </summary>
        /// <value>The root dir.</value>
        [StringRequireValidator("Root directory should be not empty")]
        public string RootDir
        {
            get { return this.SearchSetting.RootDir; }
            set { this.SearchSetting.RootDir = value; }
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        [StringRequireValidator("Filter pattern should be not empty")]
        public string Filter
        {
            get { return this.SearchSetting.NugetFilter; }
            set { this.SearchSetting.NugetFilter = value; }
        }

        #endregion

        #region Overrides of ContentBase<Nuget>

        /// <summary>
        /// Scans this instance.
        /// </summary>
        public override IList<Nuget> HandleScanning()
        {
            Logger.Debug("HandleScanning...");
            // Scanning nuget
            var results = this.nugetController.GetItems(this.SearchSetting).Select(i => new Nuget(i)).ToList();

            Logger.Debug($"HandleScanning...DONE - Items count = [{results.Count}]");
            return results;
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        public override bool HandleIncreaseVersion(IList<Nuget> selectedItems)
        {
            Logger.Debug("HandleIncreaseVersion...");
            var nuspecInfos = selectedItems.Select(n => (NuspecInfo) n).ToList();
            var increaseResult = this.nugetController.IncreaseVersion(nuspecInfos);
            foreach (var result in increaseResult)
            {
                var nugetSpec = selectedItems.FirstOrDefault(n => n.Name.Equals(result.Data.Name));
                nugetSpec?.Refresh();
            }
            Logger.Debug($"HandleIncreaseVersion...DONE - Items increase success = [{increaseResult.Count(n => n.HasError == false)}]");
            return increaseResult.All(rl => rl.HasError);
        }

        /// <summary>
        /// Filters the items by text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        protected override IList<Nuget> FilterItemsByText(string text)
        {
            Logger.Debug("FilterItemsByText...");
            if (string.IsNullOrEmpty(text))
            {
                Logger.Debug($"FilterItemsByText...DONE - Return Origin list items, count =[{this.OriginItems.Count}]");
                return this.OriginItems;
            }
            var results = this.OriginItems.Where(n => n.Name.ToLower().Contains(text.ToLower())).ToList();
            Logger.Debug($"FilterItemsByText...DONE - Items filtered = [{results.Count}]");
            return results;
        }

        #endregion
    }
}
