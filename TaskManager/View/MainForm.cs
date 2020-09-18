using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Models.Model;
using TaskManager.Models.Service;
using TaskManager.View.Interfaces;

namespace TaskManager
{
    internal partial class MainForm : Form, IMainForm
    {

        #region Поля

        private readonly Logger _Log = LogManager.GetCurrentClassLogger();
        protected ApplicationContext _Context;

        #endregion

        #region Свойства

        private bool IsUpdate { get; set; } = false;
        ObservableCollection<ProcessModel> ProcessesModel { get; set; }

        #endregion

        #region Методы

        public new void Show()
        {
            _Context.MainForm = this;
            _Log.Debug("Запуск окна Task Manager");
            Application.Run(_Context);
        }
        public async Task TaskLoad(ProcessService ProcessService)
        {
            _Log.Debug("Загрузка данных Task Manager");
            ProcessesModel = new ObservableCollection<ProcessModel>(await ProcessService.ConvertToListAsync());
            ProcessesList.DataSource = ProcessesModel;
        }

        #endregion

        #region События

        #region ButtonClick

        private async void StartStopButton_Click(object sender, System.EventArgs e)
        {
            IsUpdate = !IsUpdate;
            if (!IsUpdate)
            {
                _Log.Debug("Нажатие кнопки кнопки StopButton");
                StartStopButton.Text = "Начать обновление процессов";
            }
            else
            {
                _Log.Debug("Нажатие кнопки кнопки StartButton");
                StartStopButton.Text = "Остановить обновление процессов";
                //BackgroundWorker backgroundWorker = new BackgroundWorker
                //{
                //    WorkerReportsProgress = true
                //};
                //backgroundWorker.DoWork += BackgroundWorker_DoWork;
                //backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
                //backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                //backgroundWorker.RunWorkerAsync();
            }
            await DoWork();

        }

        #endregion

        #region RunWorkerCompleted

        //private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    _Log.Debug("Datagridview обновление данных");
        //    ProcessesList.DataSource = ProcessesModel;
        //}

        #endregion

        #region ProgressChanged


        private async Task ReportProgress()
        {
            await Task.Run(async () =>
            {
                if (!IsUpdate) return;
                while (IsUpdate)
                {
                    var result = await ProcessService.ConvertToListAsync().ConfigureAwait(false);
                    await Task.Delay(500);
                    ObservableCollection<ProcessModel> collection = new ObservableCollection<ProcessModel>(result);

                    if (this.InvokeRequired)
                        this.BeginInvoke(new Action(() =>
                        {
                            ProcessesModel = collection;
                            ProcessesList.DataSource = ProcessesModel;
                            //ProcessesList.Refresh(); Почему-то не обновляется таким способом 
                            //ProcessesList.Update();
                            Debug.WriteLine("1" + ProcessesModel.Count + " " + ProcessesList.RowCount + "\n");
                        }));
                    else
                    {
                        ProcessesModel = collection;
                        ProcessesList.DataSource = ProcessesModel;
                        //ProcessesList.Refresh();
                        //ProcessesList.Update();
                        Debug.WriteLine("2" + ProcessesModel.Count + " " + ProcessesList.RowCount + "\n");
                    }
                }
            }).ConfigureAwait(false);
        }

        #endregion

        #region DoWork
        private async Task DoWork()
        {
            if (!IsUpdate) return;
            await Task.Run( async () =>
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new Action(async () => await ReportProgress()));
                else await ReportProgress();
            }).ConfigureAwait(false);
        }

        #endregion

        #region ProcessesList_DoubleClick
        private void ProcessesList_DoubleClick(object sender, EventArgs e)
        {
            TabPanel.SelectTab(TabShowMoreProcess);
        }

        #endregion

        #region ProcessesList_Click
        private void ProcessesList_Click(object sender, EventArgs e)
        {
            var p = (ProcessModel)ProcessesList.CurrentRow.DataBoundItem;
            LabelId.Text = p.Id.ToString();
            LabelProcessName.Text = p.ProcessName;
            LabelPriority.Text = p.Priority.ToString();
        }

        #endregion

        #endregion
        public MainForm(ApplicationContext Context)
        {
            _Context = Context;
            InitializeComponent();
            _Log.Debug("Инициализация окна Task Manager");
        }
    }
}
