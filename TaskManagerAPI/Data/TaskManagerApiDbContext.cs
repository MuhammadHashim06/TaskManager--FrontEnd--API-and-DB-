using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class TaskManagerApiDbContext(DbContextOptions<TaskManagerApiDbContext> options) : DbContext(options)
    {
        public DbSet<MyTask> MyTasks { get; set; }
    }
}