using LightInject;
using System;
using System.Windows.Forms;
using TaskManager.Models.Data;
using TaskManager.Models.Service;
using TaskManager.Presenter;
using TaskManager.View.Interfaces;

namespace TaskManager
{
    static class Program
    {
        private static readonly ApplicationContext _Context = new ApplicationContext();

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new ServiceContainer();
            container
                .RegisterInstance<ApplicationContext>(_Context)
                .Register<ProcessData>()
                .Register<ProcessService>()
                .Register<IMainForm, MainForm>()
                .Register<MainFormPresenter>();
            ApplicationContoller controller = new ApplicationContoller(container);
            controller.Run<MainFormPresenter>();
        }
    }
}
