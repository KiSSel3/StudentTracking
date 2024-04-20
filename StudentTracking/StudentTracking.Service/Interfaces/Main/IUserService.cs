using System.Security.Claims;
using StudentTracking.Domain.Entities.Main;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Service.Interfaces.Main;

public interface IUserService
{
    public Task UpdateAsync(string userId, UpdateUserViewModel viewModel);
    public Task UpdatePasswordAsync(string userId, UpdatePasswordViewModel viewModel);
    public Task RegisterAsync(RegisterViewModel model);
    public Task<ClaimsIdentity> LoginAsync(LoginViewModel model);
    public Task DeleteAsyncAsync(string userId);
    
    public Task<IEnumerable<UserEntity>> GetAllAsync();
    public Task<UserEntity> GetByIdAsync(string userId);
    public Task<UserEntity> GetByLoginAsync(string login);
    
    public Task AddToRoleAsync(string userLogin, string roleName);
    public Task<bool> CheckUserRoleAsync(string userId, string roleName);

    public Task ChangeAccess(string userId);
}