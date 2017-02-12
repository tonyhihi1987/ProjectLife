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


    }
}
