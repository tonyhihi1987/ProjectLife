using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;

namespace ProjectLife.DAL
{
    public interface IProjectDataContext
    {
        Project GetProject(int id);
        List<Project> GetProjects(string filter);
        List<string> GetTypes();
        void Add(Project project);
        void Delete(int id);
        void Update(Project project,bool updated);
    }
}