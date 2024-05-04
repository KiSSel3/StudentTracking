using StudentTracking.Domain.Entities.Main;
using StudentTracking.Domain.Entities.Shared;

namespace StudentTracking.Shared.ViewModels;

public class AdminViewModel
{
    public IEnumerable<UserEntity> Users { get; set; }
    public IEnumerable<FacultyEntity> Faculties { get; set; }
}