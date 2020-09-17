
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Models.Model;

namespace TaskManager.Models.Data
{
    internal class ProcessData
    {
        public async IAsyncEnumerable<ProcessModel> GetAllProcesses()
        {
            var processes = Process.GetProcesses().ToAsyncEnumerable();
            await foreach (var item in processes)
                yield return new ProcessModel
                {
                    Id = item.Id,
                    ProcessName = item.ProcessName,
                    Priority = item.BasePriority,
                    //WorkingSet64 = item.WorkingSet64,
                    //PagedSystemMemorySize64 = item.PagedSystemMemorySize64,
                    //PagedMemorySize64 = item.PagedMemorySize64
                };
        }
    }

}
