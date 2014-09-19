using System;

namespace Peoneer.Library.Core
{
    public class Project : IProject
    {
        public Project(string name, string repositoryUri)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException("name"); }
            if (string.IsNullOrEmpty(repositoryUri)) { throw new ArgumentNullException("repositoryUri"); }
            Name = name;
        }
        public void Integrate(IIntegrationResult result)
        {
            throw new System.NotImplementedException();
        }

        public string Name { get; private set; }
        public ISourceRepository Repository { get; set; }
        public IStage[] Stages { get; set; }
    }
}