using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Peoneer.Library.Core;
using Peoneer.Library.Messages;
using Peoneer.Web.Models;

namespace Peoneer.Web.Controllers
{
    public class AgentController : Controller
    {
        private readonly IBuildAgentRepository _buildAgentRepository;

        public AgentController() : this(new InMemoryBuildAgentRepository())
        {
            
        }
        public AgentController(IBuildAgentRepository buildAgentRepository)
        {
            _buildAgentRepository = buildAgentRepository;
            _buildAgentRepository.AddAgent("http://localhost:27500/service");
        }

        public ActionResult Index()
        {
            // get registered build agents
            var registeredAgents = _buildAgentRepository.GetAgents();
            var first = registeredAgents.First();
            var registeredServers = new List<AgentViewModel> { new AgentViewModel { Endpoint = first.EndpointAddress, Name = first.Name} };
            return View(registeredServers);
        }

        public ActionResult Status(string id)
        {
            var agentRequested = _buildAgentRepository.GetAgent(id);
            if (agentRequested != null)
            {
                return View(new AgentViewModel { Name= agentRequested.Name, Endpoint = agentRequested.EndpointAddress });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Echo(string id, string message)
        {
            var agentEchoed = _buildAgentRepository.GetAgent(id);
            if (agentEchoed != null)
            {
                var request = new EchoRequest { Message = message };
                var response = agentEchoed.ProcessMessage(request) as EchoResponse;
                return PartialView("_Echo", new EchoViewModel { Request = request, Response = response });
            }
            return PartialView("_Echo");
        }
    }
}