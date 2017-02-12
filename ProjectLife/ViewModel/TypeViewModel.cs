using Microsoft.AspNetCore.Http;
using ProjectLife.Model;
using ProjectLife.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ProjectLife.ViewModel
{
    public class TypeViewModel
    {

        public int Id { get; set; }
        [Display(Name = "TypeName", ResourceType = typeof(Labels))]
        public string Name { get; set; }

    }
}
