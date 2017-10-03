namespace VSProjectManagement.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.Composition;
    using System.IO;
    using Constant;
    using FileExplorer.Contract;
    using FileExplorer.Contract.Controller;
    using FileExplorer.Contract.Model;
    using Helper;
    using Model;

    /// <summary>
    /// Class ProjectController.
    /// </summary>
    /// <seealso>
    ///     <cref>FileExplorer.Contract.Controller.IController{ProjectInfo{ReferAssemblyInfo}}</cref>
    /// </seealso>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ProjectController : IController<ProjectInfo<ReferAssemblyInfo>>
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="filterSetting">The filter setting.</param>
        /// <returns></returns>
        public IList<ProjectInfo<ReferAssemblyInfo>> GetItems(FilterSetting filterSetting)
        {
            var folders = Directory.GetDirectories(filterSetting.RootDir, "*", SearchOption.TopDirectoryOnly);
            var result = new List<ProjectInfo<ReferAssemblyInfo>>();

            foreach (var folder in folders)
            {
                var files = Directory.GetFiles(folder, filterSetting.ProjectFilter, SearchOption.TopDirectoryOnly);
                if (files.Any())
                {
                    result.AddRange(this.BuildProjectInfo(files));
                }
            }
            return result;
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        /// <returns></returns>
        public IList<ResultInfo<ProjectInfo<ReferAssemblyInfo>>> IncreaseVersion(IList<ProjectInfo<ReferAssemblyInfo>> selectedItems)
        {
            var result = new List<ResultInfo<ProjectInfo<ReferAssemblyInfo>>>();
            foreach (var pr in selectedItems)
            {
                var prResult = new ResultInfo<ProjectInfo<ReferAssemblyInfo>>(pr);
                try
                {
                    UpdateAssemblyVersion(pr);
                    pr.GetAssemblyInfo();
                    pr.Refresh();
                }
                catch (Exception ex)
                {
                    prResult.Error = ex.Message;
                }
                result.Add(prResult);
            }

            return result;
        }

        /// <summary>
        /// Builds the project information.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        private IList<ProjectInfo<ReferAssemblyInfo>> BuildProjectInfo(string[] files)
        {
            var result = new List<ProjectInfo<ReferAssemblyInfo>>();
            foreach (var f in files)
            {
                var pr = new ProjectInfo<ReferAssemblyInfo>(new FileInfo(f));
                pr.GetAssemblyInfo();
                result.Add(pr);
            }
            return result;
        }

        /// <summary>
        /// Updates the assembly version.
        /// </summary>
        /// <param name="project">The project.</param>
        private static void UpdateAssemblyVersion(ProjectInfo<ReferAssemblyInfo> project)
        {
            // Get AssemblyInfo.cs file
            if (!File.Exists(project.Path))
            {
                return;
            }

            var dirName = Path.Combine(Path.GetDirectoryName(project.Path), FileVersionInfoConst.PropertiesFolderName);
            var files = Directory.GetFiles(dirName, "AssemblyInfo.cs", SearchOption.TopDirectoryOnly);
            if (files.Any())
            {
                foreach (var file in files)
                {
                    var newVersion = VersionHelper.GetIncreasionVersion(project.AssemblyVersion);
                    UpVersion(file, newVersion);
                }
            }
            else
            {
               throw new Exception("No assembly file.");
            }
        }

        /// <summary>
        /// Updates the version.
        /// </summary>
        /// <param name="filePath">Content of the file.</param>
        /// <param name="newVersion">The new version.</param>
        private static void UpVersion(string filePath, string newVersion)
        {
            var allLines = File.ReadAllLines(filePath);
            var listIndex = new List<int>();
            var index = 0;
            foreach (var line in allLines)
            {
                if (line.StartsWith("//"))
                {
                    index++;
                    continue;
                }

                if (line.Contains(FileVersionInfoConst.AssemblyInformationalVersionConst)
                    || line.Contains(FileVersionInfoConst.AssemblyVersionConst)
                    || line.Contains(FileVersionInfoConst.AssemblyFileVersionConst)
                    )
                {
                    listIndex.Add(index);
                }
                index++;
            }

            foreach (var curIndex in listIndex)
            {
                var lineStr = allLines[curIndex];
                allLines[curIndex] = ChangeVersionInLine(lineStr, newVersion);
            }

            File.WriteAllLines(filePath, allLines, Encoding.Unicode);
        }

        /// <summary>
        /// Changes the version in line.
        /// </summary>
        /// <param name="lineStr">The value.</param>
        /// <param name="newVersion">The new version.</param>
        /// <returns>System.String.</returns>
        private static string ChangeVersionInLine(string lineStr, string newVersion)
        {
            var startIndex = lineStr.IndexOf("\"", StringComparison.Ordinal);
            var endIndex = lineStr.IndexOf("\"", startIndex + 1, StringComparison.Ordinal);
            var oldVersion = lineStr.Substring(startIndex + 1, endIndex - startIndex - 1);
            return lineStr.Replace(oldVersion, newVersion);
        }
    }
}
