using TaskManager.Models.Interfaces;

namespace TaskManager.Models.Model
{
    internal class ProcessModel : IProcess
    {
        [System.ComponentModel.Browsable(false)]
        public int Id { get; set; }
        public string ProcessName { get; set; }
        [System.ComponentModel.Browsable(false)]
        public int Priority { get; set; }
        //public long WorkingSet64 { get; set; }
        //public long PagedSystemMemorySize64 { get; set; }
        //public long PagedMemorySize64 { get; set; }
    }
}
