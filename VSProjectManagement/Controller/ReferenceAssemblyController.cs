namespace VSProjectManagement.Controller
{
    using System.ComponentModel.Composition;
    using System.IO;
    using FileExplorer.Contract;
    using FileExplorer.Contract.Controller;
    using FileExplorer.Contract.Model;
    using Helper;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class ReferenceAssemblyController.
    /// </summary>
    /// <seealso />
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ReferenceAssemblyController : IController<ProjectInfo<ReferAssemblyInfo>>
    {
        #region Implementation of IController<ProjectInfo>

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="filterSetting">The filter setting.</param>
        /// <returns>IList<ProjectInfo/>.</returns>
        public IList<ProjectInfo<ReferAssemblyInfo>> GetItems(FilterSetting filterSetting)
        {
            var result = new List<ProjectInfo<ReferAssemblyInfo>>();
            if (string.IsNullOrEmpty(filterSetting.ReferenceAssemblyFilter))
            {
                return result;
            }

            var assemblyNames = filterSetting.ReferenceAssemblyFilter.Split(',').Select(n => n.Trim()).ToList();

            assemblyNames.ForEach(assemblyName =>
            {
                var absoluteFilePath = Path.Combine(filterSetting.RootDir, assemblyName);

                if (Directory.Exists(filterSetting.RootDir))
                {
                    var rootDir = new DirectoryInfo(filterSetting.RootDir);
                    var files = rootDir.GetFiles("*.csproj", SearchOption.AllDirectories)
                                        .Where(c => this.IsReferenceProject(c.FullName, assemblyName)).ToList();

                    if (files.Any())
                    {
                        result.Add(this.BuildReferAssemblyInfo(assemblyName, absoluteFilePath, files));
                    }
                }
            });
            return result;
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        /// <returns>IList<ResultInfo/><ProjectInfo/><ReferAssemblyInfo/>>>.</returns>
        public IList<ResultInfo<ProjectInfo<ReferAssemblyInfo>>> IncreaseVersion(IList<ProjectInfo<ReferAssemblyInfo>> selectedItems)
        {
            return null;
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        /// <param name="newVersion">The new version.</param>
        /// <returns>IList<ResultInfo/><ProjectInfo/><ReferAssemblyInfo/>>>.</returns>
        public IList<ResultInfo<ProjectInfo<ReferAssemblyInfo>>> IncreaseVersion(IList<ProjectInfo<ReferAssemblyInfo>> selectedItems, string newVersion)
        {
            var resultInfo = new List<ResultInfo<ProjectInfo<ReferAssemblyInfo>>>();
            foreach (var item in selectedItems)
            {
                var referAssemblyName = item.Name;
                foreach (var childItem in item.Items)
                {
                    var assemblyResult = new ResultInfo<ProjectInfo<ReferAssemblyInfo>>(childItem)
                    {
                        SourceProjectName = referAssemblyName
                    };

                    try
                    {
                        var changResult = VSProjectHelper.ChangeReferenceVersion(childItem.Path, referAssemblyName, newVersion);
                        if (!changResult)
                        {
                            assemblyResult.Error = $"Can not change reference version for assembly {referAssemblyName} with version=[{newVersion}]";
                        }
                    }
                    catch (Exception ex)
                    {
                        assemblyResult.Error = ex.Message;
                    }
                    resultInfo.Add(assemblyResult);
                }
            }
            return resultInfo;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Builds the nuget information.
        /// </summary>
        /// <param name="assemblyName">Name of the file.</param>
        /// <param name="path">The path.</param>
        /// <param name="listItems">The list items.</param>
        /// <returns>IList<NuspecInfo></NuspecInfo>.</returns>
        private ProjectInfo<ReferAssemblyInfo> BuildReferAssemblyInfo(string assemblyName, string path, List<FileInfo> listItems)
        {
            var referenceAssemblys = this.BuildItemInfo(listItems, assemblyName);
            var asemblyInfo = new ProjectInfo<ReferAssemblyInfo>
            {
                Name = assemblyName,
                Path = path,
                Items = referenceAssemblys
            };

            return asemblyInfo;
        }

        /// <summary>
        /// Determines whether [is reference project] [the specified file path].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="assemblyName">The search pattern.</param>
        /// <returns><c>true</c> if [is reference project] [the specified file path]; otherwise, <c>false</c>.</returns>
        private bool IsReferenceProject(string filePath, string assemblyName)
        {
            var result = !string.IsNullOrEmpty(VSProjectHelper.GetReferenceVersion(filePath, assemblyName));
            return result;
        }

        /// <summary>
        /// Builds the item information.
        /// </summary>
        /// <param name="sourceItem">The source item.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>IList<ItemInfo></ItemInfo>.</returns>
        public List<ReferAssemblyInfo> BuildItemInfo(IList<FileInfo> sourceItem, string assemblyName)
        {
            var result = new List<ReferAssemblyInfo>();
            if (sourceItem.Any())
            {
                var data = sourceItem.Select(i => this.BuildReferAssemblyInfo(i, assemblyName)).ToList();
                result.AddRange(data);
            }
            return result;
        }

        /// <summary>
        /// Builds the item information.
        /// </summary>
        /// <param name="sourceItem">The source item.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>ItemInfo.</returns>
        private ReferAssemblyInfo BuildReferAssemblyInfo(FileInfo sourceItem, string assemblyName)
        {
            var item = new ReferAssemblyInfo(sourceItem)
            {
                RefVersion = VSProjectHelper.GetReferenceVersion(sourceItem.FullName, assemblyName)
            };
            return item;
        }

        #endregion
    }
}
