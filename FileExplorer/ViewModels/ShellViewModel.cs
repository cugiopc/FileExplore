namespace FileExplorer.ViewModels
{
    using Caliburn.Micro;
    using System.ComponentModel.Composition;

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Conductor<Screen>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        public ShellViewModel()
        {
            this.ActiveMainPage();
        }

        /// <summary>
        /// Actives the main page.
        /// </summary>
        private void ActiveMainPage()
        {
            var mainPage = IoC.Get<MainPageViewModel>();
            this.ActivateItem(mainPage);
        }
    }
}