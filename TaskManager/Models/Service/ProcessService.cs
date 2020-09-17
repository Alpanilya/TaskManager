using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using TaskManager.Models.Base;
using TaskManager.Models.Data;
using TaskManager.Models.Model;

namespace TaskManager.Models.Service
{
    internal class ProcessService : Service<ProcessModel>
    {
        private readonly ManagementEventWatcher _StartWatch, _StopWatch;
        protected static IAsyncEnumerable<ProcessModel> ProcessesEnumAsync => _ProcessData.GetAllProcesses();
        public ProcessService(ProcessData ProcessData)
        {
            _ProcessData = ProcessData;

            _StartWatch = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            _StopWatch = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));

            _StartWatch.EventArrived += StartWatch_EventArrived;
            _StartWatch.Start();

            _StopWatch.EventArrived += StopWatch_EventArrived;
            _StopWatch.Start();
        }
        public static async Task<List<ProcessModel>> ConvertToListAsync()
        {
            return await ProcessesEnumAsync.ToListAsync().ConfigureAwait(false);
        }
        public override void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var newProc = e.NewEvent.Properties["ProcessName"].Value.ToString();
            _Log.Debug($"Открытие нового процесса: {newProc}");
        }
        public override void StopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var oldProc = e.NewEvent.Properties["ProcessName"].Value.ToString();
            _Log.Debug($"Открытие нового процесса: {oldProc}");
        }
    }
}
