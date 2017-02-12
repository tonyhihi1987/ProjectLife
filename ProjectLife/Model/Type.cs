using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLife.Model
{
    [Table("Type")]
    public class Type : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects {get;set;}
    }
}
