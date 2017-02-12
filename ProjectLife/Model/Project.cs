using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLife.Model
{
    [Table("Project")]
    public class Project:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public int TypeID { get; set; }
        [ForeignKey("TypeID")]
        public virtual Type Type { get; set; }
        public DateTime TargetDate { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
        public string UserName { get; set; }

    }
}
