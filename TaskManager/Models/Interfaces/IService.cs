using System.Management;

namespace TaskManager.Models.Interfaces
{
    interface IService<T> where T: IProcess
    {
        void StopWatch_EventArrived(object sender, EventArrivedEventArgs e);
        void StartWatch_EventArrived(object sender, EventArrivedEventArgs e);
    }
}
