using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Shared.Models;

namespace StudentTracking.Shared.Mappers;

public class LetterToFullLetterMapper(
    IStudentRepository studentRepository,
    IRemoteAreaRepository remoteAreaRepository,
    ISpecialtyRepository specialtyRepository,
    ICountRepository countRepository,
    IBaseRepository<CompanyEntity> companyRepository,
    IBaseRepository<FacultyEntity> facultyRepository)
{
    private readonly IStudentRepository _studentRepository = studentRepository;
    private readonly IRemoteAreaRepository _remoteAreaRepository = remoteAreaRepository;
    private readonly ISpecialtyRepository _specialtyRepository = specialtyRepository;
    private readonly ICountRepository _countRepository = countRepository;
    
    private readonly IBaseRepository<CompanyEntity> _companyRepository = companyRepository;
    private readonly IBaseRepository<FacultyEntity> _facultyRepository = facultyRepository;

    public async Task<FullLetterModel> MapTo(LetterEntity letter)
    {
        FullLetterModel fullLetter = new FullLetterModel();

        fullLetter.Letter = letter;

        fullLetter.Students = await _studentRepository.GetByLetterIdAsync(letter.Id);
        fullLetter.RemoteAreas = await _remoteAreaRepository.GetByLetterIdAsync(letter.Id);
        fullLetter.Specialties = await _specialtyRepository.GetByLetterIdAsync(letter.Id);
        fullLetter.Counts = await _countRepository.GetByLetterIdAsync(letter.Id);
        
        fullLetter.Company = await _companyRepository.GetByIdAsync(letter.CompanyId);
        fullLetter.Faculty = await _facultyRepository.GetByIdAsync(letter.FacultyId);

        return fullLetter;
    }
}