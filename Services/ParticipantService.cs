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

        public void CreateGroupParticipant(int groupId, Participant participant)
        {
            if (participant.Id == 0)
            {   
                var participantRepository = _unitOfWork.Repository<Participant>();
                participantRepository.Insert(participant);
                _unitOfWork.Commit();
            }
            var groupParticipant = new GroupParticipant();
            groupParticipant.GroupId = groupId;
            groupParticipant.ParticipantId = participant.Id;
            groupParticipant.IsActive = true;

            _unitOfWork.Repository<GroupParticipant>().Insert(groupParticipant);
            _unitOfWork.Commit();
        }
    }
}
