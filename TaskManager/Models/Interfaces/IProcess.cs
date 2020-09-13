
namespace TaskManager.Models.Interfaces
{
    interface IProcess
    {
        int Id { get; }
        string ProcessName { get; }
        int Priority { get; }
    }
}
