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
            return  Projects.Include(p => p.Image).Include(p => p.Tasks).Where(a => a.Id.Equals(id)).FirstOrDefault();
            
        } 

        public List<Project> GetProjects()
        {

            var result = Projects.Include(p => p.Image).Include(p => p.Tasks).ToList();
            if (Filter.UserFilter != null && Filter.UserFilter != UsersConst.AnyWay)
            {
                result= result.Where(p => p.UserName.Equals(Filter.UserFilter)).ToList();
            }
                        
            if (Filter.TypeFilters.Where(a=>a.IsChecked).Any())
            {
                result= result.Where(a => Filter.TypeFilters.Where (f => f.IsChecked && f.Name.Equals(a.TypeName)).Count() > 0).ToList();
            }
            return result;
            
        }

        public void Add(Project project)
        {
            Projects.Add(project);
            SaveChanges();
        }
        public void Update(Project project,bool updated=false)
        {
            if (project.Image != null)
                Entry(project.Image).State = updated? EntityState.Modified : EntityState.Added;            

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
