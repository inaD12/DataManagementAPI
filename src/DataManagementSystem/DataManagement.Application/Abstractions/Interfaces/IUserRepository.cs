using DataManagement.Domain.DTOs;
using DataManagement.Domain.Entities;

namespace DataManagement.Application.Abstractions.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<UserWithRole?> GetByNameWithRoleAsync(string Name);
    }
}