using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Schools;
using Application.Schools.Type;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SchoolType, CommonDto>()
           // .ForMember(dto => dto.IsActive, s => s.MapFrom(t => t.Active))
            .ReverseMap();

             CreateMap<DocumentType, CommonDto>()
           // .ForMember(dto => dto.IsActive, s => s.MapFrom(t => t.Active))
            .ReverseMap();

            CreateMap<EntityType, CommonDto>()
            //.ForMember(dto => dto.IsActive, s => s.MapFrom(t => t.Active))
            .ReverseMap();

            CreateMap<StudentType, CommonDto>()
            //.ForMember(dto => dto.IsActive, s => s.MapFrom(t => t.Active))
            .ReverseMap();

            CreateMap<School, SchoolDto>()
           // .ForMember(dto => dto.IsActive, s => s.MapFrom(s => s.Active))
            .ForMember(dto => dto.SchoolTypeId, s => s.MapFrom(s => s.SchoolType.Id))
            .ForMember(dto => dto.SchoolTypeName, s => s.MapFrom(s => s.SchoolType.Name));

            CreateMap<AddSchoolDto, School>();
         // .ForMember(dto => dto.Active, s => s.MapFrom(s => s.IsActive));
        }
    }
}