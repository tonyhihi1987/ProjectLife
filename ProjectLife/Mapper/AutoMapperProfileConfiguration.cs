using System;
using System.Collections.Generic;
using System.Linq;
using ProjectLife.Model;
using ProjectLife.ViewModel;
using AutoMapper;

namespace ProjectLife.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Image != null ? $"data:image/gif;base64,{Convert.ToBase64String(src.Image.Data)}" : null))
                .ForMember(dest => dest.ImageId, opts => opts.MapFrom(src => src.Image.Id));
            CreateMap<ProjectViewModel, Project>().ForMember(dest => dest.CreationDate, opts => opts.MapFrom(src => DateTime.Now));
            CreateMap<ProjectLife.Model.Item, TaskViewModel>();
            CreateMap<TaskViewModel, ProjectLife.Model.Item>().ForMember(dest => dest.IsDone, opts => opts.MapFrom(src => !src.IsDone));
        }
    }
}
