using Microsoft.EntityFrameworkCore;
using ProjectLife.Model;
using System.Collections.Generic;

namespace ProjectLife.DAL
{
    public interface ITypeDataContext
    {
        Type GetType(int id);
        List<Type> GetTypes();
    }
}