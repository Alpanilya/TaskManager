using System;
using TaskManager.Models.Service;
using TaskManager.Presenter.Interfaces;
using TaskManager.View.Interfaces;

namespace TaskManager.Presenter
{
    internal class MainFormPresenter : IPresenter
    {
        private readonly IMainForm _View;
        private readonly ProcessService _ProcessSerivice;
        private readonly ApplicationContoller _ApplicationController;

        public MainFormPresenter(IMainForm View, ProcessService ProcessSerivice, ApplicationContoller ApplicationController)
        {
            _View = View;
            _ProcessSerivice = ProcessSerivice;
            _ApplicationController = ApplicationController;
        }
        public void Run()
        {
            _View.Show();
        }
    }
}
