using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Contract;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.Domain.Entities.Letter;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Shared.Models;
using StudentTracking.Shared.ViewModels;

namespace StudentTracking.Service.Implementations.Letter;

public class LetterService(
    ILogger<LetterService> logger,
    ILetterRepository letterRepository,
    IBaseRepository<CompanyEntity> companyRepository,
    ICountRepository countRepository,
    IBaseRepository<FacultyEntity> facultyRepository,
    IStudentRepository studentRepository,
    IRemoteAreaRepository remoteAreaRepository,
    ISpecialtyRepository specialtyRepository,
    IAnnualNumberPeopleRepository annualNumberPeopleRepository,
    IContractRepository contractRepository) : ILetterService
{
    private readonly ILetterRepository _letterRepository = letterRepository;
    private readonly IContractRepository _contractRepository = contractRepository;
    private readonly IAnnualNumberPeopleRepository _annualNumberPeopleRepository = annualNumberPeopleRepository;
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
            HandleError("Letter not found");
        }

        currentLetter.Number = updateLetterFormViewModel.Number ?? "";
        currentLetter.Date = updateLetterFormViewModel.Date;
        currentLetter.Position = updateLetterFormViewModel.Position ?? "";
        currentLetter.AccommodationProvision = updateLetterFormViewModel.AccommodationProvision ?? "";
        currentLetter.Note = updateLetterFormViewModel.Note ?? "";
        currentLetter.Base = updateLetterFormViewModel.Base ?? "";
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
                if (!string.IsNullOrEmpty(item))
                {
                    await _studentRepository.CreateAsync(new StudentEntity
                        { Name = item, LetterId = currentLetter.Id });
                }
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
                if (!string.IsNullOrEmpty(item))
                {
                    await _remoteAreaRepository.CreateAsync(new RemoteAreaEntity()
                        { Value = item, LetterId = currentLetter.Id });
                }
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
                if (!string.IsNullOrEmpty(item))
                {
                    await _specialtyRepository.CreateAsync(new SpecialtyEntity()
                        { Value = item, LetterId = currentLetter.Id });
                }
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
                if (!string.IsNullOrEmpty(item))
                {
                    await _studentRepository.CreateAsync(new StudentEntity { Name = item, LetterId = newLetter.Id });
                }
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
                if (!string.IsNullOrEmpty(item))
                {
                    await _remoteAreaRepository.CreateAsync(new RemoteAreaEntity()
                        { Value = item, LetterId = newLetter.Id });
                }
            }
        }

        if (newLetterFormViewModel.Specialities is not null)
        {
            foreach (var item in newLetterFormViewModel.Specialities)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    await _specialtyRepository.CreateAsync(new SpecialtyEntity()
                        { Value = item, LetterId = newLetter.Id });
                }
            }
        }
    }

    public async Task DeleteLetterAsync(Guid id)
    {
        var currentLetter = await _letterRepository.GetByIdAsync(id);
        if (currentLetter is null)
        {
            HandleError("Letter not found");
        }

        var students = await _studentRepository.GetByLetterIdAsync(id);
        foreach (var item in students)
        {
            await _studentRepository.DeleteAsync(item);
        }

        var counts = await _countRepository.GetByLetterIdAsync(id);
        foreach (var item in counts)
        {
            await _countRepository.DeleteAsync(item);
        }

        var remoteAreas = await _remoteAreaRepository.GetByLetterIdAsync(id);
        foreach (var item in remoteAreas)
        {
            await _remoteAreaRepository.DeleteAsync(item);
        }

        var specialities = await _specialtyRepository.GetByLetterIdAsync(id);
        foreach (var item in specialities)
        {
            await _specialtyRepository.DeleteAsync(item);
        }

        await _letterRepository.DeleteAsync(currentLetter);
    }

    public async Task<string> CheckLetterAsync(Guid id)
    {
        DateTime currentDate = DateTime.Now;
        int inLetterCount = 0;
        int inContractCount = 0;

        var fullLetters = await GetFullLetterListAsync();
        var fullLetter = fullLetters.FirstOrDefault(fl => fl.Letter.Id == id);
        if (fullLetter is null)
        {
            HandleError("FullLetter not found");
        }

        inLetterCount = fullLetter.Counts.Sum(c => c.Value);

        var contract = await _contractRepository.GetByCompanyIdAsync(fullLetter.Company.Id);
        var counts = await _annualNumberPeopleRepository.GetByContactId(contract.Id);
        var curYearCounts = counts.Where(c => c.Year.Year == currentDate.Year);
        inContractCount = curYearCounts.Sum(cyc => cyc.Count);

        return
            $"Количество людей в письме: {inLetterCount}\nКоличество людей в договоре на {currentDate.Year} год: {inContractCount}";
    }

    public async Task<Stream> WriteToFile()
    {
        var fullLetters = await GetFullLetterListAsync();

        using (var package = new ExcelPackage())
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Letters");
            
            worksheet.Cells[1, 1].Value = "Факультет";
            worksheet.Cells[1, 2].Value = "Специальность";
            worksheet.Cells[1, 3].Value = "Организация";
            worksheet.Cells[1, 4].Value = "Адрес";
            worksheet.Cells[1, 5].Value = "Отсталый р-н";
            worksheet.Cells[1, 6].Value = "Базовая";
            worksheet.Cells[1, 7].Value = "№ письма";
            worksheet.Cells[1, 8].Value = "Дата";
            worksheet.Cells[1, 9].Value = "Кол-во";
            worksheet.Cells[1, 10].Value = "Должность";
            worksheet.Cells[1, 11].Value = "Общежитие";
            worksheet.Cells[1, 12].Value = "Именное";
            worksheet.Cells[1, 13].Value = "Примичание";

            int row = 2;
            foreach (var fullLetter in fullLetters)
            {
                worksheet.Cells[row, 1].Value = fullLetter.Faculty.Abbreviation;
                
                string specialties = string.Join("\n", fullLetter.Specialties.Select(s => s.Value));
                worksheet.Cells[row, 2].Value = specialties;
                
                worksheet.Cells[row, 3].Value = fullLetter.Company.ShortName;
                worksheet.Cells[row, 4].Value = fullLetter.Company.Address;
                
                string remoteAreas = string.Join("\n", fullLetter.RemoteAreas.Select(r => r.Value));
                worksheet.Cells[row, 5].Value = remoteAreas;
                
                worksheet.Cells[row, 6].Value = fullLetter.Letter.Base;
                worksheet.Cells[row, 7].Value = fullLetter.Letter.Number;
                worksheet.Cells[row, 8].Value = fullLetter.Letter.Date;
                
                string counts = string.Join("\n", fullLetter.Counts.Select(c => c.Value));
                worksheet.Cells[row, 9].Value = counts;
                
                worksheet.Cells[row, 10].Value = fullLetter.Letter.Position;
                worksheet.Cells[row, 11].Value = fullLetter.Letter.AccommodationProvision;
                
                worksheet.Cells[row, 12].Value = fullLetter.Letter.AccommodationProvision;
                
                string students = string.Join("\n", fullLetter.Students.Select(s => s.Name));
                worksheet.Cells[row, 13].Value = students;
                
                worksheet.Cells[row, 1].Value = fullLetter.Letter.Note;

                ++row;
            }
            
            worksheet.Cells.AutoFitColumns();
            
            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            
            stream.Position = 0;

            return stream;
        }
    }
}