﻿using AutoMapper;
using CarCatalogService.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.Services.UserService.Models;

public class AddUserModel
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}

public class AddUserModelProfile : Profile
{
    public AddUserModelProfile()
    {
        CreateMap<AddUserModel, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login));
    }
}
