using Microsoft.AspNetCore.Mvc;
using SplitExpenses.Entities;
using SplitExpenses.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitExpenses.Controllers
{
    [ApiController]
    [Route("api/expense")]
    public class ExpenseController : Controller
    {
        private ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [Route("expenses")]
        [HttpGet]
        public ActionResult<List<Expense>> GetAllExpense()
        {
            return _expenseService.GetAllExpense();
        }

        [Route("id/{id}")]
        [HttpGet]
        public ActionResult<Expense> GetExpenseById(int id)
        {
            return _expenseService.GetExpenseById(id);
        }

        [HttpGet]
        [Route("groupId/{groupId}")]
        public ActionResult<List<Expense>> GetExpensesByGroup(int groupId)
        {
            return _expenseService.GetExpenseByGroup(groupId);
        }
    }
}
