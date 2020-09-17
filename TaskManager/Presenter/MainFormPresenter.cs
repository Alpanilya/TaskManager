using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Models.Model;
using TaskManager.Models.Service;
using TaskManager.Presenter.Interfaces;
using TaskManager.View.Interfaces;

namespace TaskManager.Presenter
{
    internal class MainFormPresenter : IPresenter
    {
        private readonly IMainForm _View;
        private readonly ProcessService _ProcessService;
        private readonly ApplicationContoller _ApplicationController;
        public MainFormPresenter(IMainForm View, ProcessService ProcessSerivice, ApplicationContoller ApplicationController)
        {
            _View = View;
            _ProcessService = ProcessSerivice;
            _ApplicationController = ApplicationController;
        }
        public async Task Run()
        {
            await _View.TaskLoad(_ProcessService);
            _View.Show();
        }
    }
}
