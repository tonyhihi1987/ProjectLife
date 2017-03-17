using ProjectLife.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectLife.Model
{
public class Filter
    {

        public static string UserFilter { get; set; } = UsersConst.AnyWay;

        public static List<UserViewModel> UserTaskFilter { get; set; } = new List<UserViewModel>();

        public static List<TypeFilterViewModel> TypeFilters { get; set; } = new List<TypeFilterViewModel>();

        public static PaginatedList<ProjectViewModel> CurrentFilteredProjects { get; set; }
        
    }
    
}
