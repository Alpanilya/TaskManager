using System;
using System.Threading.Tasks;
using TaskManager.Models.Base;
using TaskManager.Models.Model;

namespace TaskManager.Models.Service
{
    internal class ProcessService : Service<ProcessModel>
    {
        public override Task ConvertToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
