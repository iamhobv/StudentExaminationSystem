using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace StudentExamSystem.Services
{
    public static class MapperServices
    {
        public static IMapper Mapper { get; set; }

        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source)
        {
            return source.ProjectTo<TDestination>(Mapper.ConfigurationProvider);
        }


        public static IEnumerable<TDestination> ProjectEnumrableTo<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            return source.AsQueryable().ProjectTo<TDestination>(Mapper.ConfigurationProvider);
        }


        public static TDestination Map<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSourse, TDestination>(this TSourse source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
