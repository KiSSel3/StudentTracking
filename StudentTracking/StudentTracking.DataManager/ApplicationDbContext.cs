using Microsoft.EntityFrameworkCore;
using StudentTracking.Domain.Entities.Contract;
using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Domain.Entities.Main;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Domain.Models;

namespace StudentTracking.DataManager;

public class ApplicationDbContext : DbContext
{
    //Letter
    public DbSet<LetterEntity> Letters { get; set; }
    public DbSet<CountEntity> Counts { get; set; }
    public DbSet<RemoteAreaEntity> RemoteAreas { get; set; }
    public DbSet<SpecialtyEntity> Specialties { get; set; }
    public DbSet<StudentEntity> Students { get; set; }
    
    //Contract
    public DbSet<ContractEntity> Contracts { get; set; }
    public DbSet<AnnualNumberPeople> AnnualNumberPeoples { get; set; }
    
    //Shared
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<FacultyEntity> Faculties { get; set; }
    
    //Main
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleModel> UsersRoles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        /*Database.EnsureDeleted();
        Database.EnsureCreated();*/
    }

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