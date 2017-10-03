namespace FileExplorer.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Caliburn.Micro;
    using Contract;
    using Interface;
    using MaterialDesignThemes.Wpf;
    using Model;
    using Views;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PropertyChanged;
    using VSProjectManagement.Helper;

    /// <summary>
    /// Class ContentBase.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Caliburn.Micro.Screen" />
    [AddINotifyPropertyChangedInterface]
    public abstract class ContentBase<T> : Screen where T : ICheckedNode
    {
        /// <summary>
        /// The is selected all
        /// </summary>
        private bool? isSelectedAll;

        /// <summary>
        /// The header base
        /// </summary>
        private readonly IHeaderBase headerBase = IoC.Get<IHeaderBase>();

        /// <summary>
        /// Gets or sets the origin items.
        /// </summary>
        /// <value>The origin items.</value>
        public IList<T> OriginItems { get; set; } = new List<T>();

        /// <summary>
        /// Gets or sets the search setting.
        /// </summary>
        /// <value>
        /// The search setting.
        /// </value>
        public FilterSetting SearchSetting { get; private set; }

        /// <summary>
        /// Gets or sets the folders.
        /// </summary>
        /// <value>The folders.</value>
        public ObservableCollection<T> Items { get; set; } = new ObservableCollection<T>();

        /// <summary>
        /// Sets a value indicating whether this instance is all items selected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is all items selected; otherwise, <c>false</c>.
        /// </value>
        public bool? IsAllItemsSelected
        {
            set
            {
                this.isSelectedAll = value;
                this.CheckedAll(value);
            }

            get { return this.isSelectedAll; }
        }

        

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBase{T}"/> class.
        /// </summary>
        public ContentBase()
        {
            this.IsAllItemsSelected = false;
            this.SearchSetting = this.headerBase.SearchSetting;
            this.headerBase.SearchTextChangedEvent += this.OnSearchTextChangedEvent;
        }

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            base.OnActivate();
        }

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        /// <param name="close">Indicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            this.headerBase.SearchTextChangedEvent -= this.OnSearchTextChangedEvent;
        }

        /// <summary>
        /// Called when [search text changed event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="text">The text.</param>
        private void OnSearchTextChangedEvent(object sender, string text)
        {
            this.HandleSearchChanged(text);
        }

        /// <summary>
        /// Handles the search changed.
        /// </summary>
        /// <param name="text">The text.</param>
        protected virtual void HandleSearchChanged(string text)
        {
            var filteredItems = this.FilterItemsByText(text);

            // Determine should be reload data on GUI or NOT
            var needReloadData = this.IsChangeData(filteredItems);
            if (needReloadData)
            {
                Execute.OnUIThreadAsync(() =>
                {
                    this.ClearDataSource();
                    foreach (var item in filteredItems)
                    {
                        this.AddItem(item);
                    }
                });
            }
            }

        /// <summary>
        /// Scans this instance.
        /// </summary>
        public async void Scan()
        {
            // Show BusyIndicator
            var busyView = new BusyIndicator();
            await DialogHost.Show(busyView, "RootDialog", (sender, args) =>
            {
                Task.Run<IList<T>>(() =>
                {
                    if (!Directory.Exists(this.SearchSetting.RootDir))
                    {
                        return null;
                    }

                    // Clear source
                    this.ClearDataSource();
                    this.IsAllItemsSelected = false; // Uncheck selected all
                    var items = this.HandleScanning();
                    this.OriginItems = items;
                    
                    // Filter items
                    return this.FilterItemsByText(this.headerBase.FilterText);
                }).ContinueWith(tks =>
                {
                    Execute.OnUIThreadAsync(() =>
                    {
                        var result = tks.Result;
                        if (result != null)
                        {
                            foreach (var itemInfo in result)
                            {
                                this.AddItem(itemInfo);
                            }
                        }

                        DialogHost.CloseDialogCommand.Execute(true, busyView);
                    });
                });
            }, null);
        }

        /// <summary>
        /// Increases the version.
        /// </summary>
        public async void IncreaseVersion()
        {
            // Show BusyIndicator
            var busyView = new BusyIndicator();
            await DialogHost.Show(busyView, "RootDialog", (sender, args) =>
            {
                Task.Run<bool>(() =>
                {
                    var selectedItems = this.GetSelectedItems();
                    return this.HandleIncreaseVersion(selectedItems);
                }).ContinueWith(tks =>
                {
                    Execute.OnUIThreadAsync(() =>
                    {
                        DialogHost.CloseDialogCommand.Execute(true, busyView);
                    });
                });
            }, null);
        }

        /// <summary>
        /// Scans this instance.
        /// </summary>
        public abstract IList<T> HandleScanning();

        /// <summary>
        /// Increases the version.
        /// </summary>
        public abstract bool HandleIncreaseVersion(IList<T> selectedItems);

        /// <summary>
        /// Filters the items by text.
        /// </summary>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        protected abstract IList<T> FilterItemsByText(string text);

        /// <summary>
        /// Checked all.
        /// </summary>
        /// <param name="isChecked">if set to <c>true</c> [is checked].</param>
        public void CheckedAll(bool? isChecked)
        {
            foreach (var it in this.Items)
            {
                it.IsChecked = isChecked == true;
            }
        }

        /// <summary>
        /// Gets the selected projects.
        /// </summary>
        /// <returns></returns>
        protected IList<T> GetSelectedItems()
        {
            return this.Items.Where(pr => pr.IsChecked != false).ToList();
        }

        /// <summary>
        /// Clears the data source.
        /// </summary>
        protected void ClearDataSource()
        {
            if (this.Items.Count > 0)
            {
                Execute.OnUIThread(() =>
                {
                    this.Items.Clear();
                });
            }
        }

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="item">The project.</param>
        protected void AddItem(T item)
        {
            this.Items.Add(item);
        }

        /// <summary>
        /// Needs the reload data.
        /// </summary>
        protected virtual bool IsChangeData(IList<T> filteringItems)
        {
            // Diff number of element
            if (filteringItems.Count != this.Items.Count)
            {
                return true;
            }

            // Equal number element but diff inner data
            var listFilteringItemName = filteringItems.Select(n => n.Name).ToList();
            var listCurItemName = this.Items.Select(n => n.Name).ToList();
            if (VSProjectHelper.Equals(listCurItemName, listFilteringItemName))
            {
                return true;
            }
            return false;
        }
    }
}
