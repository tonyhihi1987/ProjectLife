using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;

namespace ProjectLife.DAL
{
    public interface IProjectDataContext
    {
        Project GetProject(int id);
        List<Project> GetProjects();
        void Add(Project project);
    }
}