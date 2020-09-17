using System;

namespace TaskManager.Models.Interfaces
{
    interface IProcess
    {
        public int Id { get; }
        public string ProcessName { get; }
        public int Priority { get; }
        //public long WorkingSet64 { get; }
        //public long PagedSystemMemorySize64 { get; }
        //public long PagedMemorySize64 { get; }
    }
}
