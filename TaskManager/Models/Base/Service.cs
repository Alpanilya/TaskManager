using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models.Data;
using TaskManager.Models.Interfaces;

namespace TaskManager.Models.Base
{
    internal abstract class Service<T> : IService<T> where T : IProcess
    {
        protected static ProcessData _ProcessesData;
        public List<T> ProcessesList { get; set; }
        public abstract Task ConvertToListAsync();
    }
}
