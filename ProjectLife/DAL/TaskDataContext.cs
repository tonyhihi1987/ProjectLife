using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;
using System.Linq;


namespace ProjectLife.DAL
{
    public class TaskDataContext :ProjectLifeDataContext, ITaskDataContext
    {
        public DbSet<Item> Tasks { get; set; }

        public Item GetTask(int id)
        {
            return Tasks.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public List<Item> GetTasks()
        {
            return Tasks.ToList();
        }

        public void Delete(List<Item> tasks)
        {
            var taskToDelete = new List<Item>();
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

        public void Add(List<Item> tasks)
        {

            AddRange(tasks);

            SaveChanges();
        }

    }
}
