using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLife.Model
{
    [Table("Image")]
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data {get;set;}
    }
}
