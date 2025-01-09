namespace SIAG.CrossCutting.Interfaces
{
    public interface IMappingService
    {
        TDestination Map<TSource, TDestination>(TSource source);
        List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);
    }
}
