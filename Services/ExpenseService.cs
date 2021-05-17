using SplitExpenses.Entities;
using SplitExpenses.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitExpenses.Services
{
    public class ExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Expense> GetAllExpense()
        {
            return _unitOfWork.Repository<Expense>().GetAll().ToList();
        }

        public Expense GetExpenseById(int id)
        {
            return _unitOfWork.Repository<Expense>().GetById(id);
        }

        public List<Expense> GetExpenseByGroup(int groupId)
        {
            return _unitOfWork.Repository<Expense>().FindBy(x => x.GroupId == groupId).ToList();
        }

        public void CreateExpense(Expense expense)
        {
            _unitOfWork.Repository<Expense>().Insert(expense);
            _unitOfWork.Commit();
        }
    }
}
