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
    public class LogonViewModel
    {
        [Required(ErrorMessage ="Le Nom est requis")]
        
        [Display(Name = "UserName", ResourceType = typeof(Labels))]
        public string UserName { get; set; }

        [Required(ErrorMessage  = "Le mot de passe est requis")]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }
    }
}


   
