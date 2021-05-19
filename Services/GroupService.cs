using SplitExpenses.Entities;
using SplitExpenses.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitExpenses.Services
{
    public class GroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Group> GetAllGroup()
        {
           return _unitOfWork.Repository<Group>().GetAll().ToList();
        }

        public Group GetGroupById(int id)
        {
            return _unitOfWork.Repository<Group>().GetById(id);
        }

        public void CreateGroup(Group group)
        {
            _unitOfWork.Repository<Group>().Insert(group);
            _unitOfWork.Commit();
        }

        public List<Group> GetGroupsByParticipant(int participantId)
        {
            var groupParticipant = _unitOfWork.Repository<GroupParticipant>().FindBy(x => x.ParticipantId == participantId && x.IsActive == true).ToList();
            var groupIds = groupParticipant.Select(x => x.GroupId);
            return _unitOfWork.Repository<Group>().FindBy(x => groupIds.Contains(x.Id)).ToList();

        }
    }
}
