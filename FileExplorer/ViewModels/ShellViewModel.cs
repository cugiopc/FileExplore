namespace FileExplorer.ViewModels
{
    using Caliburn.Micro;
    using System.ComponentModel.Composition;
    using ILog = log4net.ILog;
    using LogManager = log4net.LogManager;

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Conductor<Screen>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ShellViewModel));

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        public ShellViewModel()
        {
            Logger.Debug("ShellViewModel...");
            this.ActiveMainPage();
            Logger.Debug("ShellViewModel...Done");
        }

        /// <summary>
        /// Actives the main page.
        /// </summary>
        private void ActiveMainPage()
        {
            Logger.Debug("ActiveMainPage...");
            var mainPage = IoC.Get<MainPageViewModel>();
            this.ActivateItem(mainPage);
            Logger.Debug("ActiveMainPage...Done");
        }
    }
}