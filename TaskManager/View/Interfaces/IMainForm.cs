using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Model;
using TaskManager.Models.Service;
using TaskManager.Presenter;

namespace TaskManager.View.Interfaces
{
    interface IMainForm: IView
    {
        Task TaskLoad(ProcessService ProcessService);
    }
}
