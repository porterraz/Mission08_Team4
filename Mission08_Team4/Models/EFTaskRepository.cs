using Microsoft.EntityFrameworkCore;

namespace Mission08_Team4.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;

        public EFTaskRepository(TaskContext context)
        {
            _context = context;
        }

        public IQueryable<TaskItem> Tasks => _context.TaskItems.Include(t => t.Category);

        public IQueryable<Category> Categories => _context.Categories;

        public void AddTask(TaskItem task)
        {
            // Persist a newly created task record.
            _context.TaskItems.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskItem task)
        {
            // Save edits to an existing task.
            _context.TaskItems.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(TaskItem task)
        {
            // Remove a task that the user no longer needs.
            _context.TaskItems.Remove(task);
            _context.SaveChanges();
        }
    }
}
