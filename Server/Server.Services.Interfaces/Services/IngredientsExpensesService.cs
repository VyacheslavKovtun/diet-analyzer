using Server.Domain.Core.Entities;
using Server.Infrastructure.Business.AutoMapper;
using Server.Infrastructure.Business.DTO;
using Server.Infrastructure.Data.UnitOfWork;
using Server.Services.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Services
{
    public class IngredientsExpensesService : IIngredientsExpensesService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public IngredientsExpensesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewIngredientsExpenseAsync(IngredientsExpenseDTO ingredientsExpense)
        {
            var exp = mapper.Mapper.Map<IngredientsExpense>(ingredientsExpense);

            await unitOfWork.IngredientsExpensesRepository.CreateAsync(exp);
        }

        public async Task DeleteIngredientsExpenseAsync(int id)
        {
            await unitOfWork.IngredientsExpensesRepository.DeleteAsync(id);
        }

        public async Task<List<IngredientsExpenseDTO>> GetAllIngredientsExpensesAsync()
        {
            var exp = await unitOfWork.IngredientsExpensesRepository.GetAllAsync();

            return mapper.Mapper.Map<List<IngredientsExpenseDTO>>(exp);
        }

        public async Task<IngredientsExpenseDTO> GetIngredientsExpenseByIdAsync(int id)
        {
            var exp = await unitOfWork.IngredientsExpensesRepository.GetAsync(id);

            return mapper.Mapper.Map<IngredientsExpenseDTO>(exp);
        }

        public async Task<List<IngredientsExpenseDTO>> GetIngredientsExpensesByUserIdAsync(Guid id)
        {
            var exp = await GetAllIngredientsExpensesAsync();

            return exp.FindAll(e => e.UserId == id);
        }

        public async Task<IngredientsExpenseDTO> GetIngredientsExpenseByIngredientBaseInfoIdAsync(int infoId, Guid userId)
        {
            var exp = await GetAllIngredientsExpensesAsync();

            return exp.Find(e => e.IngredientId == infoId && e.UserId == userId);
        }

        public async Task UpdateIngredientsExpenseAsync(IngredientsExpenseDTO ingredientsExpense)
        {
            var exp = mapper.Mapper.Map<IngredientsExpense>(ingredientsExpense);

            await unitOfWork.IngredientsExpensesRepository.UpdateAsync(exp);
        }
    }
}
