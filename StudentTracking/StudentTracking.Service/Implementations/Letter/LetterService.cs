using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.Logging;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Shared.Models;
using StudentTracking.Shared.ViewModels;

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

    public async Task UpdateLetterAsync(UpdateLetterFormViewModel updateLetterFormViewModel)
    {
        var currentLetter = await _letterRepository.GetByIdAsync(updateLetterFormViewModel.Id);
        if (currentLetter is null)
        {
            throw new Exception("Letter not found");
        }

        currentLetter.Number = updateLetterFormViewModel.Number;
        currentLetter.Date = updateLetterFormViewModel.Date;
        currentLetter.Position = updateLetterFormViewModel.Position;
        currentLetter.AccommodationProvision = updateLetterFormViewModel.AccommodationProvision;
        currentLetter.Note = updateLetterFormViewModel.Note;
        currentLetter.Base = updateLetterFormViewModel.Base;
        currentLetter.FacultyId = updateLetterFormViewModel.FacultyId;
        currentLetter.CompanyId = updateLetterFormViewModel.CompanyId;

        await _letterRepository.UpdateAsync(currentLetter);

        var students = await _studentRepository.GetByLetterIdAsync(updateLetterFormViewModel.Id);
        foreach (var item in students)
        {
            await _studentRepository.DeleteAsync(item);
        }

        if (updateLetterFormViewModel.Students is not null)
        {
            foreach (var item in updateLetterFormViewModel.Students)
            {
                await _studentRepository.CreateAsync(new StudentEntity { Name = item, LetterId = currentLetter.Id });
            }
        }

        var counts = await _countRepository.GetByLetterIdAsync(updateLetterFormViewModel.Id);
        foreach (var item in counts)
        {
            await _countRepository.DeleteAsync(item);
        }
        if (updateLetterFormViewModel.Counts is not null)
        {
            foreach (var item in updateLetterFormViewModel.Counts)
            {
                await _countRepository.CreateAsync(new CountEntity() { Value = item, LetterId = currentLetter.Id });
            }
        }

        var remoteAreas = await _remoteAreaRepository.GetByLetterIdAsync(updateLetterFormViewModel.Id);
        foreach (var item in remoteAreas)
        {
            await _remoteAreaRepository.DeleteAsync(item);
        }
        if (updateLetterFormViewModel.RemoteAreas is not null)
        {
            foreach (var item in updateLetterFormViewModel.RemoteAreas)
            {
                await _remoteAreaRepository.CreateAsync(new RemoteAreaEntity()
                    { Value = item, LetterId = currentLetter.Id });
            }
        }

        var specialities = await _specialtyRepository.GetByLetterIdAsync(updateLetterFormViewModel.Id);
        foreach (var item in specialities)
        {
            await _specialtyRepository.DeleteAsync(item);
        }
        if (updateLetterFormViewModel.Specialities is not null)
        {
            foreach (var item in updateLetterFormViewModel.Specialities)
            {
                await _specialtyRepository.CreateAsync(new SpecialtyEntity()
                    { Value = item, LetterId = currentLetter.Id });
            }
        }
    }

    public async Task CreateLetterAsync(NewLetterFormViewModel newLetterFormViewModel)
    {
        var newLetter = new LetterEntity()
        {
            Number = newLetterFormViewModel.Number ?? "",
            Date = newLetterFormViewModel.Date,
            Position = newLetterFormViewModel.Position ?? "",
            AccommodationProvision = newLetterFormViewModel.AccommodationProvision ?? "",
            Note = newLetterFormViewModel.Note ?? "",
            Base = newLetterFormViewModel.Base ?? "",
            FacultyId = newLetterFormViewModel.FacultyId,
            CompanyId = newLetterFormViewModel.CompanyId
        };

        await _letterRepository.CreateAsync(newLetter);
        
        if (newLetterFormViewModel.Students is not null)
        {
            foreach (var item in newLetterFormViewModel.Students)
            {
                await _studentRepository.CreateAsync(new StudentEntity { Name = item, LetterId = newLetter.Id });
            }
        }
        
        if (newLetterFormViewModel.Counts is not null)
        {
            foreach (var item in newLetterFormViewModel.Counts)
            {
                await _countRepository.CreateAsync(new CountEntity() { Value = item, LetterId = newLetter.Id });
            }
        }

        if (newLetterFormViewModel.RemoteAreas is not null)
        {
            foreach (var item in newLetterFormViewModel.RemoteAreas)
            {
                await _remoteAreaRepository.CreateAsync(new RemoteAreaEntity()
                    { Value = item, LetterId = newLetter.Id });
            }
        }

        if (newLetterFormViewModel.Specialities is not null)
        {
            foreach (var item in newLetterFormViewModel.Specialities)
            {
                await _specialtyRepository.CreateAsync(new SpecialtyEntity()
                    { Value = item, LetterId = newLetter.Id });
            }
        }
    }
}