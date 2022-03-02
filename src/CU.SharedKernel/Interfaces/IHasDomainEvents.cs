using CU.SharedKernel.Base;

namespace CU.SharedKernel.Interfaces
{
    public interface IHasDomainEvents
    {
        List<DomainEventBase> DomainEvents { get; }
    }
}
