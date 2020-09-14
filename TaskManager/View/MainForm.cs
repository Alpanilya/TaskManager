using NLog;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Models.Model;
using TaskManager.Models.Service;
using TaskManager.Presenter;
using TaskManager.View.Interfaces;

namespace TaskManager
{
    internal partial class MainForm : Form, IMainForm
    {
        protected ApplicationContext _Context;
        private Logger log = LogManager.GetCurrentClassLogger();
        public MainForm(ApplicationContext Context)
        {
            _Context = Context;
            InitializeComponent();
            log.Debug("Инициализация окна Task Manager");
        }
        public new void Show()
        {
            _Context.MainForm = this;
            log.Debug("Запуск окна Task Manager");
            Application.Run(_Context);
        }
        public async Task TaskLoad(ProcessService ProcessService)
        {
            log.Debug("Загрузка данных Task Manager");
            await ProcessService.ConvertToListAsync();
            ProcessesList.DataSource = ProcessService.ProcessesList;
        }
        private void StartStopButton_Click(object sender, System.EventArgs e)
        {
            log.Debug("Нажатие кнопки кнопки StartStopButton");
            //ProcessesList.Refresh();
            //ProcessesList.Update();
        }
    }
}
