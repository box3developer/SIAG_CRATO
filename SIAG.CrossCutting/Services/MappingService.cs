using AutoMapper;
using SIAG.CrossCutting.Interfaces;

namespace SIAG.CrossCutting.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public virtual List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return source.Select(item => _mapper.Map<TSource, TDestination>(item)).ToList();
        }
    }
}
