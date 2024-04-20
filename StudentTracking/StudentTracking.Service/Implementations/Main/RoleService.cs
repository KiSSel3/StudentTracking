using StudentTracking.DataManager.Interfaces.Main;
using StudentTracking.Domain.Entities.Main;
using StudentTracking.Service.Interfaces.Main;

namespace StudentTracking.Service.Implementations.Main;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<IEnumerable<RoleEntity>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();

        return roles;
    }

    public async Task<RoleEntity> CreateAsync(string name)
    {
        var roleByName = await _roleRepository.GetByNameAsync(name);
        if(roleByName != null)
        {
            throw new Exception("A role already exists");
        }

        var newRole = new RoleEntity { Name = name };

        await _roleRepository.CreateAsync(newRole);

        return newRole;
    }

    public async Task<RoleEntity> UpdateAsync(Guid roleId, string name)
    {
        var roleByName = await _roleRepository.GetByNameAsync(name);
        if (roleByName != null)
        {
            throw new Exception("A role already exists");
        }

        var roleById = await _roleRepository.GetByIdAsync(roleId);
        if (roleById == null)
        {
            throw new Exception("No role");
        }

        roleById.Name = name;

        await _roleRepository.UpdateAsync(roleById);

        return roleById;
    }

    public async Task<RoleEntity> DeleteAsync(Guid roleId)
    {
        var roleById = await _roleRepository.GetByIdAsync(roleId);
        if(roleById == null)
        {
            throw new Exception("No role");
        }

        await _roleRepository.DeleteAsync(roleById);

        return roleById;
    }

    public async Task<RoleEntity> GetByIdAsync(Guid roleId)
    {
        var roleById = await _roleRepository.GetByIdAsync(roleId);
        if (roleById == null)
        {
            throw new Exception("No role");
        }

        return roleById;
    }

    public async Task<RoleEntity> GetByNameAsync(string name)
    {
        var roleByName = await _roleRepository.GetByNameAsync(name);
        if (roleByName == null)
        {
            throw new Exception("No role");
        }

        return roleByName;
    }
}