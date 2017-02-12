using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Image!= null? $"data:image/gif;base64,{Convert.ToBase64String(src.Image.Data)}" :null));
            CreateMap<ProjectViewModel, Project>().ForMember(dest => dest.CreationDate, opts => opts.MapFrom(src => DateTime.Now));
            CreateMap<TypeViewModel, ProjectLife.Model.Type>();
            CreateMap<ProjectLife.Model.Type, TypeViewModel>();
            CreateMap<TaskViewModel, ProjectLife.Model.Task>();            
        }
    }
}
