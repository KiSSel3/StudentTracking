namespace StudentTracking.Domain.Entity.Main;

public class UserEntity : BaseEntity
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}