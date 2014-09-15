using Peoneer.Library.Messages;

namespace Peoneer.Web.Models
{
    public class AgentViewModel
    {
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public EchoViewModel EchoViewModel { get; set; }
    }

    public class EchoViewModel
    {
        public EchoRequest Request { get; set; }
        public EchoResponse Response { get; set; }
    }
}