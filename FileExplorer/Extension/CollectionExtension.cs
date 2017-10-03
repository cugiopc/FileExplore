using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Extension
{
    using System.Collections.ObjectModel;

    public static class CollectionExtension
    {
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void AddRange<T>(this ObservableCollection<T> target, IList<T> source)
        {
            foreach (var item in source)
            {
                target.Add(item);
            }
        }
    }
}
