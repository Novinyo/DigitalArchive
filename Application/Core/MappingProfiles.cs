using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Dtos;
using Application.Documents.Type;
using Application.Schools;
using Application.Schools.Type;
using Application.Staffs.Dtos;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SchoolType, CommonDto>()
            .ReverseMap();

             CreateMap<DocumentType, DocumentTypeDto>()
            .ReverseMap();

            CreateMap<StaffType, EntityTypeDto>();
           // .ForMember(dto => dto.SchoolName, s => s.MapFrom(t => t.School.Name));
            
             CreateMap<EntityTypeAddDto, StaffType>();

            CreateMap<StudentType, EntityTypeDto>()
            .ForMember(dto => dto.SchoolName, s => s.MapFrom(t => t.School.Name));
            
            CreateMap<EntityTypeAddDto, StudentType>();

            CreateMap<AppUser, UserStaffDto>()
            .ForMember(x => x.Avatar, u => u.MapFrom(u => u.ProfilePicture));

            CreateMap<Staff, Staffs.StaffRDto>()
            .ForMember(s => s.SchoolId, sr => sr.MapFrom(s => s.School.Id))
            .ForMember(s => s.Birthdate, sr => sr.MapFrom(s => s.DOB))
            .ForMember(s => s.SchoolName, sr => sr.MapFrom(s => s.School.Name))
            .ForMember(s => s.SchoolCode, sr => sr.MapFrom(s => s.School.Code))
            .ForMember(s => s.StaffTypeName, sr => sr.MapFrom(s => s.StaffType.Name))
            .ForMember(s => s.StaffTypeId, sr => sr.MapFrom(s => s.StaffType.Id))
            .ForMember(s => s.UserId, u => u.MapFrom(u => u.User.Id))
            .ForMember(s => s.Avatar, u => u.MapFrom(u => u.User.ProfilePicture))
            .ForMember(s => s.Username, u => u.MapFrom(u => u.User.UserName))
            .ForMember(s => s.FirstName, u => u.MapFrom(u => u.User.FirstName))
            .ForMember(s => s.MiddleName, u => u.MapFrom(u => u.User.MiddleName))
            .ForMember(s => s.LastName, u => u.MapFrom(u => u.User.LastName))
            .ForMember(s => s.Email, u => u.MapFrom(u => u.User.Email))
            .ForMember(s => s.PhoneNumber, u => u.MapFrom(u => u.User.PhoneNumber));

            CreateMap<School, SchoolDto>()
            .ForMember(dto => dto.SchoolTypeId, s => s.MapFrom(s => s.SchoolType.Id))
            .ForMember(dto => dto.SchoolTypeName, s => s.MapFrom(s => s.SchoolType.Name));

            CreateMap<School, UserSchoolDto>()
            .ForMember(u => u.SchoolId, s => s.MapFrom(s => s.Id));

            CreateMap<AddSchoolDto, School>();

            CreateMap<UserWDto, AppUser>().
            ForMember(a => a.UserName, u => u.MapFrom(u => u.Username))
            .ForMember(a => a.ProfilePicture, u => u.MapFrom(u => u.Avatar));

            CreateMap<StaffWDto, Staff>();
        }
    }
}