using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;

namespace ProjectLife.DAL
{
    public interface ITaskDataContext
    {
        List<Item> GetTasks();
        Item GetTask(int id);
        void Delete(List<Item> tasks);
        void Add(List<Item> tasks);
    }
}