using Application.Dto.Disk;
using Application.Dto.Movie;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public static class AutoMapperConfig
{
    public static IMapper Initialize()
        => new MapperConfiguration(cfg =>
    {
            #region Movies

        cfg.CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.LastModified, act => act.MapFrom(src => src.Detail.LastModified));
        cfg.CreateMap<CreateMovieDto, Movie>();
        cfg.CreateMap<UpdateMovieDto, Movie>();

            #endregion

            #region Disks

        cfg.CreateMap<Disk, DiskDto>();
        cfg.CreateMap<CreateDiskDto, Disk>();
        cfg.CreateMap<UpdateDiskDto, Disk>();

            #endregion

        }).CreateMapper();
}
