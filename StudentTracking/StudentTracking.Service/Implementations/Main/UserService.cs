using System.Security.Claims;
using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces.Main;
using StudentTracking.DataManager.Interfaces.Supporting;
using StudentTracking.Domain.Entities.Main;
using StudentTracking.Domain.Models;
using StudentTracking.Service.Interfaces.Main;
using StudentTracking.Shared.Mappers;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Service.Implementations.Main;

public class UserService(ILogger<UserService> logger, IUserRepository userRepository,
    IRoleRepository roleRepository, IRoleUserRepository roleUserRepository) : IUserService
{
    private readonly ILogger<UserService> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IRoleUserRepository _roleUserRepository = roleUserRepository;

    private void HandleError(string message)
    {
        _logger.LogError(message);
        throw new Exception(message);
    }

    public async Task UpdateAsync(string userId, UpdateUserViewModel viewModel)
    {
        var guidUserId = StringToGuidMapper.MapTo(userId);

        var user = await _userRepository.GetByIdAsync(guidUserId);
        if (user is null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        if(user.Login != viewModel.Login)
        {
            var userByNewLogin = await _userRepository.GetByLoginAsync(viewModel.Login);
            if (userByNewLogin is not null)
            {
                HandleError("This login is already in use");
            }
        }

        user.Name = viewModel.Name;
        user.Login = viewModel.Login;
        
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdatePasswordAsync(string userId, UpdatePasswordViewModel viewModel)
    {
        var guidUserId = StringToGuidMapper.MapTo(userId);
        
        var user = await _userRepository.GetByIdAsync(guidUserId);
        if (user is null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        if (viewModel.CurrentPassword != user.PasswordHash)
        {
            HandleError("Invalid password");
        }

        user.PasswordHash = viewModel.NewPassword;
        await _userRepository.UpdateAsync(user);
    }

    public async Task RegisterAsync(RegisterViewModel model)
    {
        var user = await _userRepository.GetByLoginAsync(model.Login);
        if (user is not null)
        {
            HandleError("This login is already in use.");
        }

        user = new UserEntity()
        {
            Login = model.Login,
            Name = model.Name,
            PasswordHash = model.Password,
            IsAccessAllowed = false
        };

        await _userRepository.CreateAsync(user);

        await AddToRoleAsync(model.Login, "User");
    }

    public async Task<ClaimsIdentity> LoginAsync(LoginViewModel model)
    {
        var user = await _userRepository.GetByLoginAsync(model.Login);

        if(user is null)
        {
            HandleError("Login or password entered incorrectly.");
        }

        if (user.PasswordHash != model.Password)
        {
            HandleError("Login or password entered incorrectly.");
        }

        if (!user.IsAccessAllowed)
        {
            HandleError("Access denied. Please contact your administrator for access.");
        }

        return (await Authenticate(user));
    }

    public async Task DeleteAsyncAsync(string userId)
    {
        var guidUserId = StringToGuidMapper.MapTo(userId);
        
        var user = await _userRepository.GetByIdAsync(guidUserId);
        if (user is null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        await _userRepository.DeleteAsync(user);
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        if (users.ToList().Count == 0)
        {
            HandleError("Users are not found");
        }

        return users;
    }

    public async Task<UserEntity> GetByIdAsync(string userId)
    {
        var guidUserId = StringToGuidMapper.MapTo(userId);
        
        var user = await _userRepository.GetByIdAsync(guidUserId);
        if (user is null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        return user;
    }

    public async Task<UserEntity> GetByLoginAsync(string login)
    {
        var user = await _userRepository.GetByLoginAsync(login);
        if (user is null)
        {
            HandleError($"User with login: {login} not found");
        }

        return user;
    }

    public async Task AddToRoleAsync(string userLogin, string roleName)
    {
        var user = await _userRepository.GetByLoginAsync(userLogin);
        if (user is null)
        {
            HandleError($"User with Login: {userLogin} not found");
        }
        
        var role = await _roleRepository.GetByNameAsync(roleName);
        if(role is null)
        {
            HandleError($"Role {roleName} not found");
        }

        await _roleUserRepository.CreateAsync(new UserRoleModel() { RoleId = role.Id, UserId = user.Id });
    }

    public async Task<bool> CheckUserRoleAsync(string userId, string roleName)
    {
        var guidUserId = StringToGuidMapper.MapTo(userId);

        var user = await _userRepository.GetByIdAsync(guidUserId);
        if (user is null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        var role = await _roleRepository.GetByNameAsync(roleName);
        if(role is null)
        {
            HandleError($"Role not found");
        }

        var userRoles = await _roleUserRepository.GetByUserId(guidUserId);
        foreach(var item in userRoles)
        {
            if (item.RoleId.Equals(role.Id))
            {
                return true;
            }
        }

        return false;
    }

    public async Task ChangeAccess(string userId)
    {
        var guidUserId = StringToGuidMapper.MapTo(userId);
        
        var user = await _userRepository.GetByIdAsync(guidUserId);
        if (user is null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        user.IsAccessAllowed = !user.IsAccessAllowed;
        
        await _userRepository.UpdateAsync(user);
    }
    
    private async Task<ClaimsIdentity> Authenticate(UserEntity user)
    {   
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim("IsAdmin", (await CheckUserRoleAsync(user.Id.ToString(), "Admin")).ToString()),
            new Claim("UserId", user.Id.ToString()),
        };

        return new ClaimsIdentity(claims, "Authentication", ClaimsIdentity.DefaultNameClaimType, null);
    }
}