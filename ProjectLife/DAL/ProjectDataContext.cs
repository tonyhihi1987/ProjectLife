using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;
using System.Linq;


namespace ProjectLife.DAL
{
    public class ProjectDataContext :ProjectLifeDataContext, IProjectDataContext
    {
        public DbSet<Project> Projects { get; set; }

        public Project GetProject(int id)
        {
            return Projects.Where(a => a.Id.Equals(id)).FirstOrDefault();
        } 

        public List<Project> GetProjects()
        {
            return Projects.Include(p => p.Image).Include(p=>p.Type).ToList();
        }

        public void Add(Project project)
        {
            Projects.Add(project);
            SaveChanges();
        }


    }
}
