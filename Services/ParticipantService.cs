using SplitExpenses.Entities;
using SplitExpenses.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitExpenses.Services
{
    public class ParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParticipantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Participant> GetParticipantsByGroup(int groupId)
        {
            var groupParticipant = _unitOfWork.Repository<GroupParticipant>().FindBy(x => x.GroupId == groupId && x.IsActive == true).ToList();
            var participantIds = groupParticipant.Select(x => x.ParticipantId);
            return _unitOfWork.Repository<Participant>().FindBy(x => participantIds.Contains(x.Id)).ToList();
        }

        public void CreateParticipant(Participant participant)
        {
            _unitOfWork.Repository<Address>().Insert(participant.Address);
            _unitOfWork.Repository<Participant>().Insert(participant);
            _unitOfWork.Commit();
        }

        public void CreateGroupParticipant(GroupParticipant participant)
        {
            _unitOfWork.Repository<GroupParticipant>().Insert(participant);
            _unitOfWork.Commit();
        }
    }
}
