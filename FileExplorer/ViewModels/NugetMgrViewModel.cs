namespace FileExplorer.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using Model;
    using PropertyChanged;
    using VSProjectManagement.Controller;
    using System.Linq;
    using VSProjectManagement.Helper;
    using VSProjectManagement.Model;

    [Export]
    [AddINotifyPropertyChangedInterface]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NugetMgrViewModel : ContentBase<Nuget>
    {
        #region Variables

        /// <summary>
        /// The project controller
        /// </summary>
        private readonly NugetController nugetController = IoC.Get<NugetController>();

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
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
            // Scanning nuget
            return this.nugetController.GetItems(this.SearchSetting).Select(i => new Nuget(i)).ToList();
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        public override bool HandleIncreaseVersion(IList<Nuget> selectedItems)
        {
            var nuspecInfos = selectedItems.Select(n => (NuspecInfo) n).ToList();
            var increaseResult = this.nugetController.IncreaseVersion(nuspecInfos);
            foreach (var result in increaseResult)
            {
                var nugetSpec = selectedItems.FirstOrDefault(n => n.Name.Equals(result.Data.Name));
                nugetSpec?.Refresh();
            }

            return increaseResult.All(rl => rl.HasError);
        }

        /// <summary>
        /// Filters the items by text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        protected override IList<Nuget> FilterItemsByText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return this.OriginItems;
            }
            var results = this.OriginItems.Where(n => n.Name.ToLower().Contains(text.ToLower())).ToList();

            return results;
        }

        #endregion
    }
}
