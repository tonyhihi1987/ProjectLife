using ProjectLife.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectLife.Model
{
public class Filter
    {

        public static string UserFilter
        {
            get;set;
        }
        public static List<TypeFilterViewModel> TypeFilters
        {
            get; set;
        } = new List<TypeFilterViewModel>();
    }
    
}
