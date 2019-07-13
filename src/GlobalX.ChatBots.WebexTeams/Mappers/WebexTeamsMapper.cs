using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace GlobalX.ChatBots.WebexTeams.Mappers
{
    internal class WebexTeamsMapper : IWebexTeamsMapper
    {
        private readonly IMapper _mapper;

        public WebexTeamsMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IConfigurationProvider ConfigurationProvider => _mapper.ConfigurationProvider;
        public Func<Type, object> ServiceCtor => _mapper.ServiceCtor;

        public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);

        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts) =>
            _mapper.Map<TDestination>(source, opts);

        public TDestination Map<TSource, TDestination>(TSource source) => _mapper.Map<TSource, TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source,
            Action<IMappingOperationOptions<TSource, TDestination>> opts) => _mapper.Map(source, opts);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) =>
            _mapper.Map(source, destination);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination,
            Action<IMappingOperationOptions<TSource, TDestination>> opts) => _mapper.Map(source, destination, opts);

        public object Map(object source, Type sourceType, Type destinationType) =>
            _mapper.Map(source, sourceType, destinationType);

        public object
            Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts) =>
            _mapper.Map(source, sourceType, destinationType, opts);

        public object Map(object source, object destination, Type sourceType, Type destinationType) =>
            _mapper.Map(source, destination, sourceType, destinationType);

        public object Map(object source, object destination, Type sourceType, Type destinationType,
            Action<IMappingOperationOptions> opts) =>
            _mapper.Map(source, destination, sourceType, destinationType, opts);

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null,
            params Expression<Func<TDestination, object>>[] membersToExpand) =>
            _mapper.ProjectTo(source, parameters, membersToExpand);

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source,
            IDictionary<string, object> parameters, params string[] membersToExpand) =>
            _mapper.ProjectTo<TDestination>(source, parameters, membersToExpand);
    }
}
