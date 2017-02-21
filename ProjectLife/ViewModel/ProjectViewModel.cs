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
        [Display(Name = "Task", ResourceType = typeof(Labels))]
        public virtual ICollection<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
        public List<SelectListItem> Types { get; set; } = new List<SelectListItem>();
        public string TypeName { get; set; }

        [Display(Name = "TargetDate", ResourceType = typeof(Labels))]
        [DataType(DataType.DateTime)]
        public DateTime TargetDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public bool IsDone { get {

                return !Tasks.Where(t => !t.IsDone).Any();
                    } }
        public string TargetTime
        {
            get
            {
                TimeSpan ts = TargetDate - DateTime.Now;
                return string.Format("{0}j",
                ts.Days);
            }
        }

        public bool IsWarn
        {
            get
            {
                return (TargetDate - DateTime.Now).Days <=0;
            }
        }


        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public IFormFile File { get; set; }
        
        public int? ImageId { get; set; }

        public string Data { get; set; }

        public ProjectViewModel()
        {
            foreach (var prop in typeof(TypeConst).GetFields())
            {

                    var value = prop.GetValue(new TypeConst()) as string;
                    Types.Add(new SelectListItem()
                    {
                        Text = value,
                        Value = value,                       

                    });
                }
            }
        }

    
}
