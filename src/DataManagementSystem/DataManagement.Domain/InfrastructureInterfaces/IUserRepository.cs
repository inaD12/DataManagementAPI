﻿using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;

namespace DataManagement.Domain.InfrastructureInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<UserWithRole?> GetByNameWithRoleAsync(string Name);
    }
}