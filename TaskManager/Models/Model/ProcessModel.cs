using TaskManager.Models.Interfaces;

namespace TaskManager.Models.Model
{
    internal class ProcessModel : IProcess
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public int Priority { get; set; }
    }
}
