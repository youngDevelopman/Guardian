using AutoMapper;
using Guardian.ResourceService.Models;
using Guardian.Shared.Models.ResourceService;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Mapper
{
    public class ResourceServiceMappingProfile : Profile
    {
        public ResourceServiceMappingProfile()
        {
            // Add default values when adding a new resource
            CreateMap<AddResource, Resource>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => ObjectId.GenerateNewId().ToString()))
                .ForMember(dest => dest.Segments, opt => opt.MapFrom(x => new List<Models.ResourceSegment>()))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
