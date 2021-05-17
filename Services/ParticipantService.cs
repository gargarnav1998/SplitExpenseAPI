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
    }
}
