#if false
//TODO: Restore this when Domain Events are figured out
using CU.SharedKernel.Base;

namespace CU.SharedKernel.Interfaces
{
    public interface IHasDomainEvents
    {
        List<DomainEventBase> DomainEvents { get; }
    }
}
#endif