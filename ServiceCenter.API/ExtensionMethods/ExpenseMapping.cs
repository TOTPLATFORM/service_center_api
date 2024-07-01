using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ExpenseMapping
{
    public static void AddExpenseMapping(this MappingProfiles map)
    {
        map.CreateMap<ExpenseRequestDto, Expense>();
        map.CreateMap<Expense, ExpenseResponseDto>();
    }
}