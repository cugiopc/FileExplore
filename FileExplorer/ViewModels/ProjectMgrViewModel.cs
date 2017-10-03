namespace FileExplorer.ViewModels
{
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using Model;
    using PropertyChanged;
    using VSProjectManagement.Controller;
    using VSProjectManagement.Model;
    using System.Collections.Generic;
    using System.Linq;
    using VSProjectManagement.Helper;

    [Export]
    [AddINotifyPropertyChangedInterface]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ProjectMgrViewModel : ContentBase<Project>
    {
        #region Variables

        /// <summary>
        /// The project controller
        /// </summary>
        private readonly ProjectController projectController = IoC.Get<ProjectController>();

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public string Filter
        {
            get { return this.SearchSetting.ProjectFilter; }
            set { this.SearchSetting.ProjectFilter = value; }
        }

        #endregion

        #region Override ContentBase<Project> methods

        /// <summary>
        /// Scans this instance.
        /// </summary>
        /// <returns>IList<Project/>.</returns>
        public override IList<Project> HandleScanning()
        {
            // Scanning project
            return this.projectController.GetItems(this.SearchSetting).Select(i => new Project(i)).ToList();
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool HandleIncreaseVersion(IList<Project> selectedItems)
        {
            var ptInfoList = selectedItems.Select(pr => (ProjectInfo<ReferAssemblyInfo>)pr).ToList();
            var result = this.projectController.IncreaseVersion(ptInfoList);
            return result.All(rs => rs.HasError);
        }

        /// <summary>
        /// Filters the items by text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        protected override IList<Project> FilterItemsByText(string text)
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
