using StudentTracking.Domain.Enums;
using StudentTracking.Service.Interfaces.Contract;
using StudentTracking.Shared.Models;

namespace StudentTracking.Service.Implementations.Contract;

public class ContractSortingService : IContractSortingService
{
    public IEnumerable<FullContractModel> SortContracts(IEnumerable<FullContractModel> contracts, ContractSortingParameters sortingParameter,
        bool isSortingDirectionDesc)
    {
        switch (sortingParameter)
        {
            case ContractSortingParameters.Number:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Number) :
                    contracts.OrderBy(c => c.Number);
            
            case ContractSortingParameters.Faculty:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Faculty.Abbreviation) :
                    contracts.OrderBy(c => c.Faculty.Abbreviation);
            
            case ContractSortingParameters.CompanyShortName:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Company.ShortName) :
                    contracts.OrderBy(c => c.Company.ShortName);
            
            case ContractSortingParameters.CompanyAddress:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Company.Address) :
                    contracts.OrderBy(c => c.Company.Address);
            
            case ContractSortingParameters.CompanyFullName:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Company.FullName) :
                    contracts.OrderBy(c => c.Company.FullName);
            
            case ContractSortingParameters.EducationProfile:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Contract.EducationProfile) :
                    contracts.OrderBy(c => c.Contract.EducationProfile);
            
            case ContractSortingParameters.Director:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Company.Director) :
                    contracts.OrderBy(c => c.Company.Director);
            
            case ContractSortingParameters.Agency:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Contract.Agency) :
                    contracts.OrderBy(c => c.Contract.Agency);
            
            case ContractSortingParameters.ContractNumber:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Contract.Number) :
                    contracts.OrderBy(c => c.Contract.Number);
            
            case ContractSortingParameters.Status:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Contract.Status) :
                    contracts.OrderBy(c => c.Contract.Status);
            
            case ContractSortingParameters.StartDate:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Contract.StartDate) :
                    contracts.OrderBy(c => c.Contract.StartDate);
            
            case ContractSortingParameters.EndDate:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Contract.EndDate) :
                    contracts.OrderBy(c => c.Contract.EndDate);
            
            case ContractSortingParameters.SpecialtyCode:
                return isSortingDirectionDesc ? 
                    contracts.OrderByDescending(c => c.Contract.SpecialtyCode) :
                    contracts.OrderBy(c => c.Contract.SpecialtyCode);
            
            case ContractSortingParameters.Counts:
                return isSortingDirectionDesc ?
                    contracts.OrderByDescending(c => c.AnnualNumberPeoples.Sum(anp => anp.Count)) :
                    contracts.OrderBy(c => c.AnnualNumberPeoples.Sum(anp => anp.Count));
            default:
                throw new ArgumentException("Invalid sorting parameter", nameof(sortingParameter));
        }
    }
}