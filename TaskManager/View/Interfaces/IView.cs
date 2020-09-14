using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Presenter;

namespace TaskManager.View.Interfaces
{
    interface IView
    {
        void Show();
        void Close();
    }
}
