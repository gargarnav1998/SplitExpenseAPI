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
    [Route("api/group")]
    public class GroupController : Controller
    {
        private GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [Route("groups")]
        [HttpGet]
        public ActionResult<List<Group>> GetAllGroup()
        {
            return _groupService.GetAllGroup();
        }

        [Route("id/{id}")]
        [HttpGet]
        public ActionResult<Group> GetGroupById(int id)
        {
            return _groupService.GetGroupById(id);
        }

        [Route("participantId/{participantId}")]
        [HttpGet]
        public ActionResult<List<Group>> GetGroupsByParticipant(int participantId)
        {
            return _groupService.GetGroupsByParticipant(participantId);
        }
    }
}
