
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                    Priority = item.BasePriority
                };
        }
    }

}
