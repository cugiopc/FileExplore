using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Model
{
    using PropertyChanged;

    /// <summary>
    /// Interface ICheckedNode
    /// </summary>
    public interface ICheckedNode
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>null</c> if [is checked] contains no value, <c>true</c> if [is checked]; otherwise, <c>false</c>.</value>
        bool? IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
    }
}
