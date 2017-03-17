using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProjectLife.Model
{
    [Table("Project")]
    public class Project:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; }
        public DateTime TargetDate { get; set; }
        public virtual ICollection<Item> Tasks { get; set; }
        public string ImageName { get; set; }
        
        public string UserName { get; set; }

    }
}
