namespace Peoneer.Library.Core
{
    public interface IProject : IIntegratable
    {
        string Name { get; }
        ISourceRepository Repository { get; set; }
        IStage[] Stages { get; set; }
    }
}