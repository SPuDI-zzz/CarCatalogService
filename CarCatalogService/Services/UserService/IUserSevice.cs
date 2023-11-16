﻿using CarCatalogService.Services.UserService.Models;

namespace CarCatalogService.Services.UserService;

public interface IUserSevice
{
    Task<UserModel> GetUser(long userId);
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel> AddUser(AddUserModel model);
    Task<UserModel> UpdateUser(long userId, UpdateUserModel model);
    Task DeleteUser(long userId);
}