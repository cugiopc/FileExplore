﻿namespace FileExplorer.ViewModels
{
    using System.ComponentModel.Composition;
    using System.Linq;
    using Caliburn.Micro;
    using Extension;
    using Model;
    using PropertyChanged;
    using VSProjectManagement.Controller;
    using VSProjectManagement.Model;
    using System.Collections.Generic;
    using Validation;
    using VSProjectManagement.Helper;
    using ILog = log4net.ILog;
    using LogManager = log4net.LogManager;

    /// <summary>
    /// Class ReferenceAssMgrViewModel.
    /// </summary>
    /// <seealso cref="Project" />
    [Export]
    [AddINotifyPropertyChangedInterface]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ReferenceAssMgrViewModel : ContentBase<ReferenceAssMgrViewModel, Project>
    {
        #region Variables
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ReferenceAssMgrViewModel));

        #endregion

        #region Properties

        /// <summary>
        /// The project controller
        /// </summary>
        private readonly ReferenceAssemblyController referenceAssemblyController = IoC.Get<ReferenceAssemblyController>();

        /// <summary>
        /// Gets or sets the root dir.
        /// </summary>
        /// <value>The root dir.</value>
        [StringRequireValidatorAttribute("Root directory should be not empty")]
        public string RootDir {
            get { return this.SearchSetting.RootDir; }
            set { this.SearchSetting.RootDir = value; }
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        [StringRequireValidatorAttribute("Filter pattern should be not empty")]
        public string Filter
        {
            get { return this.SearchSetting.ProjectFilter; }
            set { this.SearchSetting.ProjectFilter = value; }
        }

        /// <summary>
        /// Gets or sets the reference ass.
        /// </summary>
        /// <value>The reference ass.</value>
        [StringRequireValidatorAttribute("Assembly name should be not empty")]
        public string ReferenceAss
        {
            get { return this.SearchSetting.ReferenceAssemblyFilter; }
            set
            {
                this.SearchSetting.ReferenceAssemblyFilter = value;
            }
        }

        /// <summary>
        /// Gets or sets the ass version.
        /// </summary>
        /// <value>The ass version.</value>
        [StringRequireValidatorAttribute("Version should be not empty")]
        [RegularValidatorAttribute(@"^([\d]+[.][\d]+[.][\d]+([.][\d]+)*)$", "Version input incorrect format")]
        public string AssemblyVersion { get; set; }
       
        #endregion

        #region Overrides of ContentBase<Project>

        /// <summary>
        /// Scans this instance.
        /// </summary>
        /// <returns>IList<Project>.</Project></returns>
        public override IList<Project> HandleScanning()
        {
            Logger.Debug("HandleScanning...");
            this.SearchSetting.ReferenceAssemblyFilter = this.ReferenceAss;
            var referenceProject = this.referenceAssemblyController.GetItems(this.SearchSetting);
            Logger.Debug("HandleScanning...DONE");
            return this.BuildReferenceInfos(referenceProject);
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool HandleIncreaseVersion(IList<Project> selectedItems)
        {
            Logger.Debug("HandleIncreaseVersion...");
            if (string.IsNullOrEmpty(this.AssemblyVersion))
            {
                return false;
            }

            var selectingItem = selectedItems.Select(n => n as ProjectInfo<ReferAssemblyInfo>).ToList();

            // Filter before execute increase
            foreach (var prjData in selectingItem)
            {
                var prjItem = selectedItems.FirstOrDefault(n => n.Name.Equals(prjData.Name)); // Binding model
                var checkingItems = prjItem?.Items.Where(n => n.IsChecked == true).ToList(); // Get checked child node
                var needExecutePrj = prjData.Items.Where(n => n.Name.Equals(checkingItems?.FirstOrDefault(i => i.Name.Equals(n.Name))?.Name)).ToList();// Data model
                prjData.Items = needExecutePrj;
            }

            var results = this.referenceAssemblyController.IncreaseVersion(selectingItem, this.AssemblyVersion);

            // Update result on GUI
            foreach (var resultItem in results)
            {
                if (!resultItem.HasError)
                {
                    var sourceProject = selectedItems.FirstOrDefault(itm => itm.Name.Equals(resultItem.SourceProjectName));
                    var childItems = sourceProject?.Items;

                    if (childItems != null)
                    {
                        foreach (var refItem in childItems)
                        {
                            if (resultItem.Data.Name.Equals(refItem.Name))
                            {
                                refItem.RefVersion = this.AssemblyVersion; // update new version on GUI
                            }
                        }
                    }
                }
            }
            Logger.Debug($"HandleIncreaseVersion...DONE - Update version success for [{results.Count(n => n.HasError == false)}] projects");
            return results.All(rs => rs.HasError);
        }

        /// <summary>
        /// Filters the items by text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        protected override IList<Project> FilterItemsByText(string text)
        {
            Logger.Debug("FilterItemsByText...");

            if (string.IsNullOrEmpty(text))
            {
                this.ClearDataSource();
                this.UpdateParentForRefProject(this.OriginItems);
                return this.OriginItems;
            }
            var projectDataList = new List<Project>();
            foreach (var project in this.OriginItems)
            {
                var matchingItems = project.Items.Where(i => i.Name.ToLower().Contains(text.ToLower())).ToList();
                if (!matchingItems.Any())
                {
                    continue;
                }
                var projectFilter = this.BuildProjectForFilter(project, matchingItems);
                projectDataList.Add(projectFilter);
            }

            Logger.Debug($"FilterItemsByText...Done - Item count = [{projectDataList.Count}]");

            return projectDataList;
        }

        /// <summary>
        /// Needs the reload data.
        /// </summary>
        /// <param name="filteringItems">The filtering items.</param>
        /// <returns><c>true</c> if [is change data] [the specified filtering items]; otherwise, <c>false</c>.</returns>
        protected override bool IsChangeData(IList<Project> filteringItems)
        {
            Logger.Debug("IsChangeData...");

            // Diff number of element
            if (filteringItems.Count != this.Items.Count)
            {
                Logger.Debug($"IsChangeData...DONE - Result=[{true}]");
                return true;
            }

            // Equal number element but diff inner data
            var listFilteringItemName = filteringItems.Select(n => n.Name).ToList();
            var listCurItemName = this.Items.Select(n => n.Name).ToList();
            if (VSProjectHelper.Equals(listCurItemName, listFilteringItemName))
            {
                // Verify child items
                for (var i = 0; i < this.Items.Count; i++)
                {
                    var filterItems = filteringItems[i].Items.Select(n => n.Name).ToList();
                    var sourceItems = this.Items[i].Items.Select(n => n.Name).ToList();
                    var compareResult = VSProjectHelper.Equals(sourceItems, filterItems);
                    if (!compareResult)
                    {
                        Logger.Debug($"IsChangeData...DONE - Result=[{true}]");
                        return true;
                    }
                }
            }
            else // Not equals
            {
                Logger.Debug($"IsChangeData...DONE - Result=[{true}]");
                return true;
            }
            Logger.Debug($"IsChangeData...DONE - Result=[{false}]");
            return false;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Builds the reference infos.
        /// </summary>
        /// <param name="referAssemblyInfos">The refer assembly infos.</param>
        /// <returns>IList<Project/>.</returns>
        private List<Project> BuildReferenceInfos(IList<ProjectInfo<ReferAssemblyInfo>> referAssemblyInfos)
        {
            Logger.Debug("BuildReferenceInfos...");

            var results = new List<Project>();
            foreach (var assemblyInfo in referAssemblyInfos)
            {
                var parentProject = new Project(assemblyInfo);
                var lstProjects = assemblyInfo.Items.Select(i => this.BuildProjectItem(i, parentProject)).ToList();

                parentProject.Items.AddRange(lstProjects);
                results.Add(parentProject);
            }
            Logger.Debug($"BuildReferenceInfos...DONE - Number project build success = [{results.Count}]");
            return results;
        }

        /// <summary>
        /// Builds the project item.
        /// </summary>
        /// <param name="projectInfo">The project information.</param>
        /// <param name="parentNode">The parent node.</param>
        /// <returns>Project.</returns>
        private RefProject BuildProjectItem(ReferAssemblyInfo projectInfo, Project parentNode)
        {
            var refPrj = new RefProject(parentNode, projectInfo)
            {
                RefVersion = projectInfo.RefVersion
            };
            return refPrj;
        }

        /// <summary>
        /// Builds the project.
        /// </summary>
        /// <param name="sourceProject">The source project.</param>
        /// <param name="refProjects">The reference projects.</param>
        /// <returns>Project.</returns>
        private Project BuildProjectForFilter(Project sourceProject, List<RefProject> refProjects)
        {
            // build data
            var prjData = sourceProject as ProjectInfo<ReferAssemblyInfo>;
           
            // build model binding
            var project = new Project(prjData);
            refProjects.ForEach(refPrj =>
            {
                refPrj.Parent = project;
                refPrj.IsChecked = false;
            });
            project.Items.AddRange(refProjects);
            return project;
        }

        /// <summary>
        /// Updates the parent for reference project.
        /// </summary>
        private void UpdateParentForRefProject(IList<Project> originItems)
        {
            // Re-Update parent for RefProject item
            foreach (var project in originItems)
            {
                foreach (var refItem in project.Items)
                {
                    refItem.IsChecked = false;
                    refItem.Parent = project;
                }
                project.IsChecked = false;
            }
        }

        #endregion
    }
}
