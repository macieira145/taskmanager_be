using CardExchange.Entities;

namespace CardExchange.Interfaces
{
    public interface ITaskRepository
    {
        ICollection<Entities.Task> GetTasks();
        Entities.Task GetTask(int id);
        bool TaskExists(int id);    
    }
}
