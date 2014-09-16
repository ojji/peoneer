using System.Collections.Generic;
using System.Linq;
using Peoneer.Library.Remote;

namespace Peoneer.Library.Core
{
    public interface IBuildAgentRepository
    {
        void AddAgent(string endpoint);
        IEnumerable<BuildAgentClientBase> GetAgents();
        BuildAgentClientBase GetAgent(string name);
    }

    public class InMemoryBuildAgentRepository : IBuildAgentRepository
    {
        private List<BuildAgentClientBase> _buildAgents = new List<BuildAgentClientBase>();
        public void AddAgent(string endpoint)
        {
            _buildAgents.Add(new WcfClient(endpoint));
        }

        public IEnumerable<BuildAgentClientBase> GetAgents()
        {
            return _buildAgents;
        }

        public BuildAgentClientBase GetAgent(string name)
        {
            return _buildAgents.FirstOrDefault(a => a.Name == name);
        }
    }
}