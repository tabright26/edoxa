using AutoMapper.Configuration;

namespace eDoxa.AutoMapper.Factories
{
    public interface IMapperFactory
    {
        MapperConfigurationExpression CreateConfiguration();
    }
}
