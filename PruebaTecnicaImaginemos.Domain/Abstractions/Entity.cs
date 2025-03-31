using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _events = new();
    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public Guid Id { get; init; }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
    {
        return _events.ToList();
    }

    public void ClearDomainEvent()
    {
        _events.Clear();
    }

    public void RaiseDomainEvent(IDomainEvent Doamin)
    {
        _events.Add(Doamin);
    }
}
