using CardExchange.Entities;

namespace CardExchange.Interfaces
{
    public interface ITaskRepository
    {
        ICollection<Entities.Task> GetTasks(int userId);
        Entities.Task GetTask(int id, int userId);
        bool TaskExists(int id, int userId);    
        bool CreateTask(Entities.Task task);
        bool UpdateTask(Entities.Task task);
        bool Save();
    }
}
