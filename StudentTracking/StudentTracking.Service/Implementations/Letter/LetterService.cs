using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Implementations.Letter;

public class LetterService(
    ILogger<LetterService> logger, ILetterRepository letterRepository,
    IBaseRepository<CompanyEntity> companyRepository, ICountRepository countRepository,
    IBaseRepository<FacultyEntity> facultyRepository, IStudentRepository studentRepository,
    IRemoteAreaRepository remoteAreaRepository, ISpecialtyRepository specialtyRepository) : ILetterService
{
    private readonly ILetterRepository _letterRepository = letterRepository;
    private readonly IBaseRepository<CompanyEntity> _companyRepository = companyRepository;
    private readonly IBaseRepository<FacultyEntity> _facultyRepository = facultyRepository;
    private readonly IStudentRepository _studentRepository = studentRepository;
    private readonly IRemoteAreaRepository _remoteAreaRepository = remoteAreaRepository;
    private readonly ISpecialtyRepository _specialtyRepository = specialtyRepository;
    private readonly ICountRepository _countRepository = countRepository;
    
    private readonly ILogger<LetterService> _logger = logger;
    
    private void HandleError(string message)
    {
        _logger.LogError(message);
        throw new Exception(message);
    }
    
    public async Task<IEnumerable<LetterEntity>> GetLetterListAsync()
    {
        var letters = await _letterRepository.GetAllAsync();
        
        return letters;
    }

    public async Task<IEnumerable<FullLetterModel>> GetFullLetterListAsync()
    {
        var letters = await _letterRepository.GetAllAsync();
        
        var fullLetters = letters.Select((letter, index) => 
        {
            var fullLetterModel = new FullLetterModel
            {
                Number = index + 1,
                
                Letter = letter,
                
                Students = _studentRepository.GetByLetterIdAsync(letter.Id).Result,
                RemoteAreas = _remoteAreaRepository.GetByLetterIdAsync(letter.Id).Result,
                Specialties = _specialtyRepository.GetByLetterIdAsync(letter.Id).Result,
                Counts = _countRepository.GetByLetterIdAsync(letter.Id).Result,
                
                Company = _companyRepository.GetByIdAsync(letter.CompanyId).Result,
                Faculty = _facultyRepository.GetByIdAsync(letter.FacultyId).Result
            };

            return fullLetterModel;
        });

        return fullLetters;
    }
}