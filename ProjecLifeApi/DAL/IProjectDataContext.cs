using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System;
using System.Collections.Generic;

namespace ProjectLife.DAL
{
    public interface IProjectDataContext:IDisposable
    {
        Project GetProject(int id);
        List<Project> GetProjects();
        List<string> GetTypes();
        int Add(Project project);
        void Delete(int id);
        void Update(Project project);
    }
}