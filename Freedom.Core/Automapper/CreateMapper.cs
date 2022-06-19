using AutoMapper;
using System;

namespace Freedom.Core.Automapper
{
    public class CreateMapper
    {

        // lo utizaba para otros framework como Prism
        [Obsolete]
        public IMapper CreateAutoMapper()
        {
            IConfigurationProvider configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile(new MapperProfiles());
                cfg.AddProfile(new MapperProfilesRTOS());
                cfg.AddProfile(new MapperProfilesSqlite());
                //cfg.ValueTransformers.Add<byte[]>(val => val.Length == 0 ? null : val);
            });

            return new Mapper(configuration);
        }
    }
}