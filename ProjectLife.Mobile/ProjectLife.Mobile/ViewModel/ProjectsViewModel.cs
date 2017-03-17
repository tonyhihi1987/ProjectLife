using ProjectLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLife.Mobile.ViewModel
{
   public class ProjectsViewModel :ViewModelBase
    {

        public List<Project> Projects = new List<Project>();        

        public ProjectsViewModel()
        {
            //using (DAL.ProjectDataContext projectDataContext = new DAL.ProjectDataContext())
            //{


            //}
        }
    }
}
