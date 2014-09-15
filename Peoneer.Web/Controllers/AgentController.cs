using System.Collections.Generic;
using System.Web.Mvc;
using Peoneer.Library.Messages;
using Peoneer.Library.Remote;
using Peoneer.Library.Utilities;
using Peoneer.Web.Models;

namespace Peoneer.Web.Controllers
{
    public class AgentController : Controller
    {
        private const string ServiceUri = "http://localhost:27500/service";
        // GET: Agent
        public ActionResult Index()
        {
            // get registered build agents
            var registeredServers = new List<AgentViewModel> { new AgentViewModel { Name = "Agent-1", Endpoint = ServiceUri} };
            return View(registeredServers);
        }

        public ActionResult Status(string id)
        {
            var agentRequested = new AgentViewModel { Name = "Agent-1", Endpoint = ServiceUri };
            return View(agentRequested);
        }

        [HttpPost]
        public ActionResult Echo(string id, string message)
        {
            
            var factory = new WcfChannelFactoryWrapper<IMessageProcessor>(ServiceUri);
            var client = factory.GetClient();
            var request = new EchoRequest {Message = message};
            var response = client.ProcessMessage(request) as EchoResponse;
            return PartialView("_Echo", new EchoViewModel() {Request = request, Response = response});
        }
    }
}