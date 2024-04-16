using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;

namespace StudentTracking.DataManager.Implementations.Letter;

public class StudentRepository(ApplicationDbContext dbContext) : IStudentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task CreateAsync(StudentEntity item)
    {
        await _dbContext.Students.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<StudentEntity> UpdateAsync(StudentEntity item)
    {
        _dbContext.Students.Update(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task DeleteAsync(StudentEntity item)
    {
        _dbContext.Students.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<StudentEntity> GetByIdAsync(Guid id)
    {
        var item = await _dbContext.Students.FindAsync(id);

        return item;
    }

    public async Task<IEnumerable<StudentEntity>> GetAllAsync()
    {
        var list = await _dbContext.Students.ToListAsync();

        return list;
    }

    public async Task<IEnumerable<StudentEntity>> GetByLetterIdAsync(Guid letterId)
    {
        var list = await _dbContext.Students.Where(s => s.LetterId == letterId).ToListAsync();
        
        return list;
    }
}