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
            return  Projects.Include(p => p.Tasks).Where(a => a.Id.Equals(id)).FirstOrDefault();
            
        } 

        public List<Project> GetProjects()
        {
            var result = Projects.Include(p => p.Tasks).ToList();

            if (Filter.UserFilter != null && Filter.UserFilter != UsersConst.AnyWay)
            {
                result= result.Where(p => p.UserName.Equals(Filter.UserFilter)).ToList();
            }
                        
            if (Filter.TypeFilters.Where(a=>a.IsChecked).Any())
            {
                result= result.Where(a => Filter.TypeFilters.Where (f => f.IsChecked && f.Name.Equals(a.TypeName)).Count() > 0).ToList();
            }

            if (Filter.UserTaskFilter.Where(a => a.IsChecked).Any())
            {

                foreach (var item in result)
                {
                    item.Tasks = item.Tasks.Where(t => Filter.UserTaskFilter.Where(f => f.IsChecked && t.UserName.Equals(f.UserName)).Count() > 0).ToList();
                }
               
            }
            return result;
        }

        public int Add(Project project)
        {
            Projects.Add(project);
            SaveChanges();
            return project.Id;
        }
        public void Update(Project project)
        {
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
