using Apartments.Entitise;
using Apartments.Models;
using AutoMapper;

namespace Apartments.Api
{
    public class MappingPostModel: Profile
    {
        public MappingPostModel()
        {
            CreateMap<ApartmentPostModel, apartment>().ReverseMap();

            CreateMap<BrokerPostModel, Broker>().ReverseMap();

            CreateMap<PatientPostModel, patient>().ReverseMap();

        }
    }
}
