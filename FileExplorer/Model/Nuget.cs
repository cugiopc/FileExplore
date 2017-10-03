using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Model
{
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using Contract;
    using PropertyChanged;
    using VSProjectManagement;
    using VSProjectManagement.Model;

    [AddINotifyPropertyChangedInterface]
    public class Nuget : NuspecInfo, ICheckedNode, INotifyPropertyChanged
    {
        public bool? IsChecked { get; set; } = false;

        public new string NugetVersion => base.NugetVersion;

        public Nuget(NuspecInfo data)
        {
            this.Path = data.Path;
            this.Name = data.Name;
            base.NugetVersion = data.NugetVersion;
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            this.OnPropertyChanged("NugetVersion");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
