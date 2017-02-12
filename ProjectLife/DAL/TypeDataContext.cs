using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;
using System.Linq;


namespace ProjectLife.DAL
{
    public class TypeDataContext : ProjectLifeDataContext, ITypeDataContext
    {
        public DbSet<Type> Types { get; set; }

        public Type GetType(int id)
        {
            return Types.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public List<Type> GetTypes()
        {
            return Types.ToList();
        }


    }
}
