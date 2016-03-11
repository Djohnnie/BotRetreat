using System.Collections.Generic;
using BotRetreat.DataTransferObjects;
using BotRetreat.Domain;

namespace BotRetreat.Mappers
{
    public interface IMapper<TEntity, TDataTransferObject>
        where TEntity : IEntity
        where TDataTransferObject : IDataTransferObject
    {
        TDataTransferObject Map(TEntity entity);

        TEntity Map(TDataTransferObject dataTransferObject);

        List<TDataTransferObject> Map(List<TEntity> entities);

        List<TEntity> Map(List<TDataTransferObject> dataTransferObjects);
    }
}