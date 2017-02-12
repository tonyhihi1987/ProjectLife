using AutoMapper;
using System.Collections.Generic;
using System.Linq;


namespace ProjectLife.MapperHelper
{
    public static class MapperHelper
    {
        public static T MapTo<T>(this object originObject,IMapper mapper)
            where T : class
        {
            return mapper.Map<T>(originObject);            
        }

        public static List<T> MapTo<T>(this IEnumerable<object> originEnumerable, IMapper mapper)
            where T : class
        {
            return originEnumerable.Select(e => e.MapTo<T>(mapper)).ToList();
        }

    }
}