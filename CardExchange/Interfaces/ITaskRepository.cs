using CardExchange.Entities;

namespace CardExchange.Interfaces
{
    public interface ITaskRepository
    {
        ICollection<Entities.Task> GetTasks(int userId);
        Entities.Task GetTask(int id, int userId);
        bool TaskExists(int id, int userId);    
        Entities.Task CreateTask(Entities.Task task);
        Entities.Task UpdateTask(Entities.Task task);
        bool DeleteTask(Entities.Task id);
        bool Save();
    }
}
