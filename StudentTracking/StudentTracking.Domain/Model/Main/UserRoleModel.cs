using StudentTracking.Domain.Entity;

namespace StudentTracking.Domain.Model.Main;

public class UserRoleModel : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}