using SplitExpenses.Entities;
using SplitExpenses.Models;
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
        private readonly GroupService _groupService;
        private readonly ParticipantService _participantService;
        public ExpenseService(IUnitOfWork unitOfWork, GroupService groupService, ParticipantService participantService)
        {
            _unitOfWork = unitOfWork;
            _groupService = groupService;
            _participantService = participantService;
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

        public List<Expense> getAllExpenseByParticipantEmail(string email)
        {
            var participant = _unitOfWork.Repository<Participant>().FindBy(p => p.EmailId == email).FirstOrDefault();
            if (participant == null)
                throw new Exception($"No Paticipant found by this email {email}");
            var transactions = _unitOfWork.Repository<Transaction>().FindBy(t => t.ParticipantId == participant.Id).ToList();
            if (transactions.Count() == 0)
                throw new Exception($"No Transaction found by email {email}");
            var expenseIds = transactions.Select(s => s.ExpenseId).Distinct().ToList();
            var expenses = _unitOfWork.Repository<Expense>().FindBy(e => expenseIds.Contains(e.Id)).ToList();
            return expenses;
        }

        public List<Expense> getAllExpenseByParticipantId(int participantId)
        {
            var participant = _unitOfWork.Repository<Participant>().FindBy(p => p.Id == participantId).FirstOrDefault();
            if (participant == null)
                throw new Exception($"No Paticipant found by this participantId {participantId}");
            var expenses = _unitOfWork.Repository<Expense>().FindBy(e => e.PaidParticipantId == participantId).ToList();
            return expenses;
        }

        public List<SplitGroupExpenses> SplitGroupExpenses(int groupId)
        {
            var data = new List<SplitGroupExpenses>();
            var group = _groupService.GetGroupById(groupId);
            if (group == null)
                throw new Exception("group not found");
            var participants = _participantService.GetParticipantsByGroup(groupId);
            if (participants.Count() == 0)
                throw new Exception("No participant found");
            var expenses = GetExpenseByGroup(groupId);
            if (expenses.Count() == 0)
                throw new Exception("no expenses added yet");
            var expenseIds = expenses.Select(s => s.Id).ToList();
            var transactions = _unitOfWork.Repository<Transaction>().FindBy(t => expenseIds.Contains(t.Id) && t.GroupId == groupId).ToList();
            if (transactions.Count() == 0)
                throw new Exception("No transactions found");
            var groupExpenses = transactions.GroupBy(g => g.ExpenseId).ToList();
            return data;
        }


    }
}
