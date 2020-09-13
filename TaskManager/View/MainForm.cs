using System.Windows.Forms;
using TaskManager.View.Interfaces;

namespace TaskManager
{
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public new void Show()
        {
            throw new System.NotImplementedException();
        }

        public void TaskLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}
