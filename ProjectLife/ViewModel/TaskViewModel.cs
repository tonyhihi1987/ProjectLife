using Microsoft.AspNetCore.Http;
using ProjectLife.Model;
using ProjectLife.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ProjectLife.ViewModel
{
    public class TaskViewModel: UserViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public bool IsDone { get; set; }
        public virtual Project Project { get; set; }

    }
}
