using CardExchange.Entities;
using CardExchange.Interfaces;

namespace CardExchange.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private TaskmanagerContext _context;

        public TaskRepository(TaskmanagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Finds an task by its ID
        /// </summary>
        /// <param name="id">Id to find</param>
        /// <returns>Returns the task if found</returns>
        public Entities.Task GetTask(int id)
        {
            return _context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Return all tasks in DB
        /// </summary>
        /// <returns>Return a list of users</returns>
        public ICollection<Entities.Task> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        /// <summary>
        /// Checks if task exists
        /// </summary>
        /// <param name="id">Id to check</param>
        /// <returns>Return true if exists or false if it does not exist</returns>
        public bool TaskExists(int id)
        {
            return _context.Tasks.Any(t => t.Id == id);
        }
    }
}
