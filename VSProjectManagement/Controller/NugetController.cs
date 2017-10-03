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
    /// Class NugetController.
    /// </summary>
    /// <seealso cref="FileExplorer.Contract.Controller.IController{VSProjectManagement.Model.NuspecInfo}" />
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NugetController : IController<NuspecInfo>
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="filterSetting">The filter setting.</param>
        /// <returns></returns>
        public IList<NuspecInfo> GetItems(FilterSetting filterSetting)
        {
            var result = new List<NuspecInfo>();
            if (Directory.Exists(filterSetting.RootDir))
            {
                var folders = Directory.GetDirectories(filterSetting.RootDir, "*", SearchOption.TopDirectoryOnly);
                

                foreach (var folder in folders)
                {
                    var files = Directory.GetFiles(folder, filterSetting.NugetFilter, SearchOption.AllDirectories);
                    if (files.Any())
                    {
                        result.AddRange(this.BuildNugetInfo(files));
                    }
                }
                return result;
            }

            return result;
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        /// <returns></returns>
        public IList<ResultInfo<NuspecInfo>> IncreaseVersion(IList<NuspecInfo> selectedItems)
        {
            var result = new List<ResultInfo<NuspecInfo>>();
            if (!selectedItems.Any())
            {
                return result;
            }
            
            foreach (var nuspecInfo in selectedItems)
            {
                var nuspecResult = new ResultInfo<NuspecInfo>(nuspecInfo);
                try
                {
                    nuspecInfo.IncreaseNugetVersion();
                    result.Add(nuspecResult);
                }
                catch (Exception ex)
                {
                    nuspecResult.Error = ex.Message;
                }
                
            }
            return result;
        }

        /// <summary>
        /// Builds the project information.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        private IList<NuspecInfo> BuildNugetInfo(string[] files)
        {
            return files.Select(f => new NuspecInfo(new FileInfo(f))).ToList();
        }
    }
}
