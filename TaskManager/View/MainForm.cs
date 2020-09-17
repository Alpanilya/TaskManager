using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        private void StartStopButton_Click(object sender, System.EventArgs e)
        {
            IsUpdate = !IsUpdate;
            Debug.WriteLine(ProcessesList.RowCount);
            if (!IsUpdate)
            {
                _Log.Debug("Нажатие кнопки кнопки StopButton");
                StartStopButton.Text = "Начать обновление процессов";
            }
            else
            {
                _Log.Debug("Нажатие кнопки кнопки StartButton");
                StartStopButton.Text = "Остановить обновление процессов";
                BackgroundWorker backgroundWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true
                };
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
                backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();
            }
        }

        #endregion

        #region RunWorkerCompleted

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _Log.Debug("Datagridview обновление данных");
            ProcessesList.DataSource = ProcessesModel;
        }

        #endregion

        #region ProgressChanged

        private async void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        var tmp = (IEnumerable<ProcessModel>)e.UserState;
                        ObservableCollection<ProcessModel> collection = new ObservableCollection<ProcessModel>(tmp);

                    if (this.InvokeRequired)
                    {
                            this.BeginInvoke(new Action(() =>
                            {
                                ProcessesModel = collection;
                                ProcessesList.DataSource = ProcessesModel;
                                Task.Delay(5000);
                            }));
                    }
                    else
                    {
                        ProcessesModel = collection;
                        ProcessesList.DataSource = ProcessesModel;
                            Task.Delay(5000);
                    }
                    }
                    catch (InvalidOperationException ex)
                    {
                        _Log.Fatal($"При изменении ObservableColletion произошла ошибка {ex}");
                    }
                }).ConfigureAwait(false);
                Debug.WriteLine("ob" + ProcessesModel.Count + ProcessesList.RowCount);
            }
        }

        #endregion

        #region DoWork

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(IsUpdate)
            {
                var result = await ProcessService.ConvertToListAsync().ConfigureAwait(false);
                e.Result = result;
                if (this.InvokeRequired)
                    this.Invoke(new Action( () =>((BackgroundWorker)sender).ReportProgress(result.Count, e.Result)));
                else ((BackgroundWorker)sender).ReportProgress(result.Count, e.Result);
            }
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
