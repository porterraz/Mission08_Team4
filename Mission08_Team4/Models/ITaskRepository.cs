using System.Linq;

namespace Mission08_TeamXXXX.Models
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> Tasks { get; }
        IQueryable<Category> Categories { get; }

        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(TaskItem task);
    }
}