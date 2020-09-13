using System.Collections.Generic;
using System.Management;
using System.Threading.Tasks;
using TaskManager.Models.Data;
using TaskManager.Models.Interfaces;

namespace TaskManager.Models.Base
{
    internal abstract class Service<T> : IService<T> where T : IProcess
    {
        protected static ProcessData _ProcessData;
        public List<T> ProcessesList { get; set; }
        public abstract Task ConvertToListAsync();
        public abstract void StopWatch_EventArrived(object sender, EventArrivedEventArgs e);
        public abstract void StartWatch_EventArrived(object sender, EventArrivedEventArgs e);
    }
}
