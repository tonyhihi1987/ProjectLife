using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;

namespace ProjectLife.DAL
{
    public interface ITaskDataContext
    {
        List<Task> GetTasks();
        Task GetTask(int id);
        void Delete(List<Task> tasks);
        void Add(List<Task> tasks);
    }
}