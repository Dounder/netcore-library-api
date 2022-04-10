using Api.DTOs;
using AutoMapper;
using Core.Entities;

namespace Api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Author, CreateAuthorDto>().ReverseMap();
    }
}
