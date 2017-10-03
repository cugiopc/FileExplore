using System;
using System.Xml;

namespace VSProjectManagement.Helper
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Constant;
    using Model;

    public static class VSProjectHelper
    {
        /// <summary>
        /// Changes the reference version.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static bool ChangeReferenceVersion(string projectFile, string assemblyName, string version)
        {
            const string xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var xmlDoc = new XmlDocument();
            XmlNamespaceManager nsMngr;

            using (var reader = new XmlTextReader(projectFile))
            {
                nsMngr = new XmlNamespaceManager(reader.NameTable);
                nsMngr.AddNamespace("a", xmlns);

                xmlDoc.Load(reader);
                reader.Dispose();
            }

            var nodes = xmlDoc.SelectNodes("//a:Reference", nsMngr);
            if (nodes != null)
            {
                foreach (XmlNode refElement in nodes)
                {
                    var inclideAtt = refElement.Attributes["Include"];
                    var data = inclideAtt.Value.Split(',');
                    var assName = data[0];

                    if (assName.Equals(assemblyName, StringComparison.CurrentCultureIgnoreCase) && data.Length >= 2)
                    {
                        var oldVersion = data[1].Split('=')[1];
                        if (string.Equals(oldVersion, version))
                        {
                            return false;
                        }
                        var newVersion = data[1].Replace(oldVersion, version);
                        
                        data[1] = newVersion;

                        var newIncludeData = String.Join(",", data);
                        inclideAtt.Value = newIncludeData;

                        // Update project version
                        xmlDoc.Save(projectFile);
                        break;
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// Gets the reference version.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static string GetReferenceVersion(string projectFile, string assemblyName)
        {
            var oldVersion = string.Empty;
            const string xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var xmlDoc = new XmlDocument();
            XmlNamespaceManager nsMngr;

            using (var reader = new XmlTextReader(projectFile))
            {
                nsMngr = new XmlNamespaceManager(reader.NameTable);
                nsMngr.AddNamespace("a", xmlns);

                xmlDoc.Load(reader);
                reader.Dispose();
            }

            var nodes = xmlDoc.SelectNodes("//a:Reference", nsMngr);
            if (nodes != null)
            {
                foreach (XmlNode refElement in nodes)
                {
                    var includeAtt = refElement.Attributes?["Include"];
                    if (includeAtt != null)
                    {
                        var data = includeAtt.Value.Split(',');
                        var assName = data[0];

                        if (assName.Equals(assemblyName, StringComparison.CurrentCultureIgnoreCase) && data.Length >= 2)
                        {
                            oldVersion = data[1].Split('=')[1];
                            break;
                        }
                    }
                }
            }

            return oldVersion;
        }

        /// <summary>
        /// Reads the nuget version.
        /// </summary>
        /// <param name="nuspecInfo">The nuspec information.</param>
        public static void BuildNugetVersion(this NuspecInfo nuspecInfo)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(nuspecInfo.Path);
            var xmlNodeList = xmlDoc.GetElementsByTagName("version");
            foreach (XmlNode node in xmlNodeList)
            {
                var version = node.FirstChild.Value;
                if (version != null)
                {
                    nuspecInfo.NugetVersion = version;
                }
            }
        }

        /// <summary>
        /// Gets the assembly information.
        /// </summary>
        /// <param name="projectInfo">The project information.</param>
        public static void GetAssemblyInfo(this ProjectInfo<ReferAssemblyInfo> projectInfo)
        {
            // Get AssemblyInfo.cs file
            var dirName = Path.GetDirectoryName(projectInfo.Path) + $@"\{FileVersionInfoConst.PropertiesFolderName}";

            if (!Directory.Exists(dirName))
            {
                return;
            }

            var files = Directory.GetFiles(dirName, FileVersionInfoConst.AssemblyFileName, SearchOption.TopDirectoryOnly);
            if (files.Any())
            {
                using (var reader = new StreamReader(files[0]))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("//"))
                        {
                            continue;
                        }

                        if (line.Contains(FileVersionInfoConst.AssemblyInformationalVersionConst))
                        {
                            projectInfo.InformationalVersion = VersionHelper.ReadAsemblyVersion(line);
                        }
                        else if (line.Contains(FileVersionInfoConst.AssemblyVersionConst))
                        {
                            projectInfo.AssemblyVersion = VersionHelper.ReadAsemblyVersion(line);
                        }
                        else if (line.Contains(FileVersionInfoConst.AssemblyFileVersionConst))
                        {
                            projectInfo.FileVersion = VersionHelper.ReadAsemblyVersion(line);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Increases the nuget version.
        /// </summary>
        /// <param name="nuspecInfo">The nuspec information.</param>
        public static void IncreaseNugetVersion(this NuspecInfo nuspecInfo)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(nuspecInfo.Path);
            var xmlNodeList = xmlDoc.GetElementsByTagName("version");
            foreach (XmlNode node in xmlNodeList)
            {
                var curVersion = node.FirstChild.Value;
                var increasingVersion = VersionHelper.GetIncreasionVersion(curVersion, 1, 3);
                if (!string.IsNullOrEmpty(curVersion))
                {
                    nuspecInfo.NugetVersion = increasingVersion;
                    node.FirstChild.Value = increasingVersion;
                }
            }

            // Save change 
            xmlDoc.Save(nuspecInfo.Path);
        }

        /// <summary>
        /// Equals the specified another list.
        /// </summary>
        /// <param name="sourceList">The source list.</param>
        /// <param name="anotherList">Another list.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Equals(List<string> sourceList, List<string> anotherList)
        {
            if (sourceList.Count != anotherList.Count)
            {
                return false;
            }

            var count = 0;
            foreach (var srcItem in sourceList)
            {
                var isContain = anotherList.Any(n => n.Equals(srcItem));
                if (!isContain)
                {
                    return false;
                }
                count++;
            }
            if (count == sourceList.Count)
            {
                return true;
            }
            return false;
        }
    }
}
