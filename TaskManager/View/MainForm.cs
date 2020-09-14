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
        public MainForm(ApplicationContext Context)
        {
            _Context = Context;
            InitializeComponent();
        }
        public new void Show()
        {
            _Context.MainForm = this;
            Application.Run(_Context);
        }
        public async Task TaskLoad(ProcessService ProcessService)
        {
            await ProcessService.ConvertToListAsync();
            ProcessesList.DataSource = ProcessService.ProcessesList;
        }
        private void StartStopButton_Click(object sender, System.EventArgs e)
        {
            //ProcessesList.Refresh();
            //ProcessesList.Update();
        }
    }
}
