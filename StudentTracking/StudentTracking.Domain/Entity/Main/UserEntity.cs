namespace StudentTracking.Domain.Entity.Main;

public class UserEntity : BaseEntity
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    
    public bool IsAccessAllowed { get; set; }
}