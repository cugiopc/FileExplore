namespace FileExplorer.ViewModels
{
    using System.ComponentModel.Composition;
    using PropertyChanged;
    using Caliburn.Micro;
    using Interface;

    [Export]
    [Export(typeof(IContentManagement))]
    [AddINotifyPropertyChangedInterface]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BodyViewModel : Conductor<Screen> , IContentManagement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyViewModel"/> class.
        /// </summary>
        public BodyViewModel()
        {
        }

        /// <summary>
        /// Shows the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public void ShowContent(Screen content)
        {
            if ( ((Screen) this.ActiveItem) != content )
            {
                this.ActivateItem(content);
            }
        }
    }
}
