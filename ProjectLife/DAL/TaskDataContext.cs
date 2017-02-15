using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;
using System.Linq;


namespace ProjectLife.DAL
{
    public class TaskDataContext :ProjectLifeDataContext, ITaskDataContext
    {
        public DbSet<Task> Tasks { get; set; }

        public Task GetTask(int id)
        {
            return Tasks.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public List<Task> GetTasks()
        {
            return Tasks.ToList();
        }

        public void Delete(List<Task> tasks)
        {
            var taskToDelete = new List<Task>();
            foreach (var item in tasks)
            {
                if (tasks.Where(a => a.Id.Equals(item.Id)).Any())
                {
                    taskToDelete.Add(item);
                }

            }                            
          RemoveRange(taskToDelete);

            SaveChanges();
        }

        public void Add(List<Task> tasks)
        {

            AddRange(tasks);

            SaveChanges();
        }

    }
}
