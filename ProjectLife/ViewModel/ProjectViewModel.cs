using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectLife.Model;
using ProjectLife.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ProjectLife.ViewModel
{
    public class ProjectViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Labels))]
        public string Description { get; set; }

        [Display(Name = "User", ResourceType = typeof(Labels))]
        public string UserName { get; set; }

        public List<SelectListItem> Users { get; set; } =
        new List<SelectListItem>()
    {
        new SelectListItem() { Text =UsersConst.Clem , Value = UsersConst.Clem },
        new SelectListItem() { Text = UsersConst.Diane, Value = UsersConst.Diane },
        new SelectListItem() { Text = UsersConst.All, Value = UsersConst.All },
    };


        public TypeViewModel Type { get; set; } = new TypeViewModel();

        [Display(Name = "TargetDate", ResourceType = typeof(Labels))]
        [DataType(DataType.DateTime)]
        public DateTime TargetDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public string TargetTime
        {
            get
            {
                TimeSpan ts = DateTime.Now - TargetDate;
                return string.Format("{0}j,{1}h,{2}m,{3}s",
                ts.Days,
                ts.Hours,
                ts.Minutes,
                ts.Seconds);
            }
        }


        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public IFormFile File { get; set; }
        
        public string Data { get; set; }



    }
}
