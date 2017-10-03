using Caliburn.Micro;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.ViewModels
{
    using Contract;
    using Interface;

    [Export]
    [AddINotifyPropertyChangedInterface]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainPageViewModel : Screen
    {
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
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageViewModel()
        {
            this.Header.SearchSetting = new FilterSetting
            {
                ProjectFilter = "*.csproj",
                NugetFilter = "*.nuspec",
                ReferenceAssemblyFilter = "SMEE.NADAE.Core.Server, SMEE.NADAE.Core",
                RootDir = @"d:\Working\ECACS_7\Implementation\"
            };

            this.Header.ProjectMgr();
        }

        #endregion
    }
}
