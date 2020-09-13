using System.Threading.Tasks;

namespace TaskManager.Models.Interfaces
{
    interface IService<T> where T: IProcess
    {
        public Task ConvertToListAsync();
    }
}
