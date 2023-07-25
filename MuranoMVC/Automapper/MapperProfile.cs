using AutoMapper;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DTOs;

namespace WebApplication10.Automapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<SearchResult, SearchResultDto>().ReverseMap();
    }
}