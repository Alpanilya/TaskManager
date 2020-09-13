using System.Windows.Forms;
using TaskManager.View.Interfaces;

namespace TaskManager
{
    public partial class MainForm : Form, IMainForm
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

        public void TaskLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}
