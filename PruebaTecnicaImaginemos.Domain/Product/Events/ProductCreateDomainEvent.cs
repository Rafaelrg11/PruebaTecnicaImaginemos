using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Product.Events;

public record ProductCreateDomainEvent(Guid productId) : IDomainEvent;
