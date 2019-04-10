using System.Collections.Generic;

using AutoMapper;
using AutoMapper.Configuration;

namespace eDoxa.AutoMapper.Factories
{
    public interface IMapperFactory
    {
        MapperConfigurationExpression CreateConfiguration();

        IEnumerable<Profile> CreateProfiles();

        IMapper CreateMapper();
    }
}
