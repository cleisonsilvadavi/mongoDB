    using System;
    using Api.Data.Collections;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;

namespace Api.Data
{

    public class MongoDB
    {
        public IMongoDatabase DB {get;}
        public MongoDB(IConfiguration configuration)
        {   
        try
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
            var client = new MongoClient(settings);
            DB = client.GetDatabase(configuration["NomeBanco"]);
            MapClasses();
        }
        catch (Exception ex) 
        {
            
            throw new MongoException("Não foi possível conectar ao MongoDB", ex);
        }        
        }


        private void MapClasses()
        {
          ConventionPack conventionPack = new ConventionPack{new CamelCaseElementNameConvention()};
          ConventionRegistry.Register("camlcase",conventionPack,t => true);
          if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
          {
            BsonClassMap.RegisterClassMap<Infectado>(i => 
            {
              i.AutoMap();
              i.SetIgnoreExtraElements(true);
            }); 
          }
        }

    }
}