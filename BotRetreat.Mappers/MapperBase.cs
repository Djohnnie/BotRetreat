using System.Collections.Generic;
using System.Linq;
using BotRetreat.DataTransferObjects;
using BotRetreat.Domain;

namespace BotRetreat.Mappers
{
    public abstract class MapperBase<TEntity, TDataTransferObject> : IMapper<TEntity, TDataTransferObject>
        where TEntity : IEntity
        where TDataTransferObject : IDataTransferObject
    {
        public abstract TDataTransferObject Map(TEntity entity);

        public abstract TEntity Map(TDataTransferObject dataTransferObject);

        public List<TDataTransferObject> Map(List<TEntity> entities)
        {
            return entities.Select(Map).ToList();
        }

        public List<TEntity> Map(List<TDataTransferObject> dataTransferObjects)
        {
            return dataTransferObjects.Select(Map).ToList();
        }
    }
}