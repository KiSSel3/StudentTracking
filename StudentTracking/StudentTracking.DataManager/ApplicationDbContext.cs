using Microsoft.EntityFrameworkCore;
using StudentTracking.Domain.Entity.Letter;
using StudentTracking.Domain.Entity.Main;
using StudentTracking.Domain.Model.Main;

namespace StudentTracking.DataManager;

public class ApplicationDbContext : DbContext
{
    //Letter
    public DbSet<FacultieEntity> Faculties { get; set; }
    public DbSet<RecordEntity> Records { get; set; }
    public DbSet<CountEntity> Counts { get; set; }
    public DbSet<RemoteAreaEntity> RemoteAreas { get; set; }
    public DbSet<SpecialtyEntity> Specialties { get; set; }
    public DbSet<StudentEntity> Students { get; set; }
    
    //Main
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleModel> UsersRoles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RoleEntity>().HasData(new RoleEntity
        {
            Id = new Guid("44546e06-8719-4ad8-b88a-f271ae9d6eab"),
            Name = "User"
        });
        
        modelBuilder.Entity<RoleEntity>().HasData(new RoleEntity
        {
            Id = new Guid("9E7BF79C-BF94-4B40-A4F6-A3660E9DFD4A"),
            Name = "Admin"
        });
    }
}