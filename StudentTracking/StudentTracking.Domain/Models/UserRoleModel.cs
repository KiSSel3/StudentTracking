namespace StudentTracking.Domain.Models;

public class UserRoleModel : BaseModel
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}