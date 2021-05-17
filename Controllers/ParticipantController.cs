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
    [Route("api/participant")]
    public class ParticipantController : Controller
    {
        private ParticipantService _pService;

        public ParticipantController(ParticipantService pService)
        {
            _pService = pService;
        }

        [Route("groupId/{groupId}")]
        [HttpGet]
        public ActionResult<List<Participant>> GetParticipantsByGroup(int groupId)
        {
            return _pService.GetParticipantsByGroup(groupId);
        }
    }
}
