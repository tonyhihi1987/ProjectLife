using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProjectLife.Model
{
    [Table("Task")]
    public class Item:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public bool IsDone { get; set; }
        public string UserName { get; set; }
        public virtual Project Project { get; set; }
    }
}
