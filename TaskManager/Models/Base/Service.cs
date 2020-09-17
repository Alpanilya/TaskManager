using NLog;
using System.Collections.Generic;
using System.Management;
using TaskManager.Models.Data;
using TaskManager.Models.Interfaces;

namespace TaskManager.Models.Base
{
    internal abstract class Service<T> : IService<T> where T : IProcess
    {
        protected static ProcessData _ProcessData;
        protected Logger _Log = LogManager.GetCurrentClassLogger();
        public abstract void StopWatch_EventArrived(object sender, EventArrivedEventArgs e);
        public abstract void StartWatch_EventArrived(object sender, EventArrivedEventArgs e);
    }
}
