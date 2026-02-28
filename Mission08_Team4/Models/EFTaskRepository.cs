using Microsoft.EntityFrameworkCore;

namespace Mission08_Team4.Models
{
    // EF Core implementation
    public class EFTaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;

        public EFTaskRepository(TaskContext context)
        {
            _context = context;
        }

        // Always include related Category data
        public IQueryable<TaskItem> Tasks => _context.TaskItems.Include(t => t.Category);

        public IQueryable<Category> Categories => _context.Categories;

        public void AddTask(TaskItem task)
        {
            _context.TaskItems.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskItem task)
        {
            _context.TaskItems.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(TaskItem task)
        {
            _context.TaskItems.Remove(task);
            _context.SaveChanges();
        }
    }
}
