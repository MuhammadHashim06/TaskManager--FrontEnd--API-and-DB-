using Microsoft.AspNetCore.Http.HttpResults;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public static class TaskManagerAPIService
    {
        public static List<MyTask> GetAll(TaskManagerApiDbContext dbContext)
        {
            var DbOutout = dbContext.MyTasks.ToList();
            return DbOutout;
        }
        public static MyTask GetById(TaskManagerApiDbContext dbContext, int id)
        {
            var RequiredTask = dbContext.MyTasks.FirstOrDefault(x => x.Id == id) ?? new MyTask();
            return RequiredTask;
        }
        public static void Add(TaskManagerApiDbContext dbContext, string tital, string description, string status)
        {
            dbContext.MyTasks.Add(new MyTask() { Title = tital, Description = description, Status = status });
            dbContext.SaveChanges();
        }
        public static void Update(TaskManagerApiDbContext dbContext, int id, string tital, string description, string status)
        {
            var targetTask = dbContext.MyTasks.FirstOrDefault(x => x.Id == id) ?? new MyTask();
            if (targetTask.Id == 0)
            {
                return;
            }
            targetTask.Title = tital;
            targetTask.Description = description;
            targetTask.Status = status;
            dbContext.SaveChanges();
        }
        public static void DeleteById(TaskManagerApiDbContext dbContext, int id)
        {
            var taskToRemove = dbContext.MyTasks.FirstOrDefault(x => x.Id == id) ?? new MyTask();
            if (taskToRemove.Id == 0)
            {
                return;
            }
            dbContext.MyTasks.Remove(taskToRemove);
            dbContext.SaveChanges();
        }
    }
}