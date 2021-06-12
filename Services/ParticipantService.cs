﻿using SplitExpenses.Entities;
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

        public Participant GetParticipantBYUserNameOrEmailId(string userName)
        {
            if (userName == null || userName.Length == 0)
                throw new Exception("Invalid UserName");
            var existingParticipant = new Participant();
            var participant = _unitOfWork.Repository<Participant>().FindBy(p => p.EmailId == userName).FirstOrDefault();
            if (participant != null)
                existingParticipant = participant;
            else
            {
                participant = _unitOfWork.Repository<Participant>().FindBy(p => p.ExtraInfo1 == userName).FirstOrDefault();
                if (participant != null)
                    existingParticipant = participant;
            }
            return existingParticipant;
        }

        public bool CheckPassword(string password)
        {
            bool result;
            if (password == null || password.Length == 0)
                throw new Exception("Invalid password");
            var participant = _unitOfWork.Repository<Participant>().FindBy(p => p.ExtraInfo2 == password).FirstOrDefault();
            if (participant == null)
                result = false;
            else
                result = true;
            return result;
        }

      public bool  GetParticipantByMobile(int mobile)
        {
            bool result;
            if (mobile.ToString().Length == 0)
                throw new Exception("Invalid mobile number");
            var participant = _unitOfWork.Repository<Participant>().FindBy(p => p.Mobile == mobile).FirstOrDefault();
            if (participant == null)
                result = false;
            else
                result = true;
            return result;
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
