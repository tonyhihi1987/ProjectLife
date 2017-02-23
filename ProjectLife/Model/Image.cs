using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProjectLife.Model
{
    [Table("Image")]
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project
        {
            get; set;
        }
    }
}
