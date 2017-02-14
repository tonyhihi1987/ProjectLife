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
            return Projects.Include(p => p.Image).Where(a => a.Id.Equals(id)).FirstOrDefault();
        } 

        public List<Project> GetProjects()
        {
            return Projects.Include(p => p.Image).ToList();
        }

        public void Add(Project project)
        {
            Projects.Add(project);
            SaveChanges();
        }
        public void Update(Project project)
        {
            if (project.Image != null)
                Entry(project.Image).State = project.ImageId != 0 ? EntityState.Modified : EntityState.Added;

            Projects.Update(project);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var project = GetProject(id);
            Projects.Remove(project);
            SaveChanges();
        }

        public List<string> GetTypes()
        {
            return Projects.Select(a => a.TypeName).Distinct().ToList();

        }

    }
}
