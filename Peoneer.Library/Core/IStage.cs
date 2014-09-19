using Peoneer.Library.Tasks;

namespace Peoneer.Library.Core
{
    public interface IStage : IIntegratable
    {
        ITask[] Tasks { get; set; }
    }
}