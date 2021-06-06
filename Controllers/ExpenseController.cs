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

        [Route("")]
        [HttpPost]
        public ActionResult CreateExpense(Expense expense)
        {
            _expenseService.CreateExpense(expense);
            return Ok();
        }


        [HttpGet]
        [Route("groupId/{groupId}")]
        public ActionResult<List<Expense>> GetExpensesByGroup(int groupId)
        {
            return _expenseService.GetExpenseByGroup(groupId);
        }

        //get expenses by emailId
        [Route("participant/email")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<List<Expense>> GetExpensesByParticipantEmail(string email)
        {
            try
            {
                var expenses = _expenseService.getAllExpenseByParticipantEmail(email);
                return expenses;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        //get expenses by participantId
        [Route("participant/id")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<List<Expense>> GetExpensesByParticipantId(int participantId)
        {
            try
            {
                var expenses = _expenseService.getAllExpenseByParticipantId(participantId);
                return expenses;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }
    }

}
