using AutoMapper;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<User, UserViewModel>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

        }
    }
}
