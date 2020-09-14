using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Logger log = LogManager.GetCurrentClassLogger();
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
        public async override Task ConvertToListAsync()
        {
            log.Debug("Конвертация IAsyncEnumerable в List");
            ProcessesList = await _ProcessData.GetAllProcesses().ToListAsync();
        }
        public async override void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var proc = e.NewEvent.Properties["ProcessName"].Value;
            log.Debug($"Открытие нового процесса: {proc}");
            await ConvertToListAsync();
            Debug.WriteLine(ProcessesList.Count);
        }
        public async override void StopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var proc = e.NewEvent.Properties["ProcessName"].Value;
            log.Debug($"Закрытие процесса: {proc}");
            await ConvertToListAsync();
            Debug.WriteLine(ProcessesList.Count);
        }
    }
}
