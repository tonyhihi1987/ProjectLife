using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectLife.Model;
using ProjectLife.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProjectLife.ViewModel
{
    public class UserViewModel:CheckViewModel
    {
       
        [Display(Name = "User", ResourceType = typeof(Labels))]
        public string UserName { get; set; }

        public List<SelectListItem> Users { get; set; } =
        new List<SelectListItem>()
    {
        new SelectListItem() { Text =UsersConst.Clem , Value = UsersConst.Clem },
        new SelectListItem() { Text = UsersConst.Diane, Value = UsersConst.Diane },
        new SelectListItem() { Text = UsersConst.All, Value = UsersConst.All },
    };
        
    }    
}
