using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.sale_detail.Events;

public record DetailSaleCreateDomainEvent(Guid saleDetailId) : IDomainEvent;
