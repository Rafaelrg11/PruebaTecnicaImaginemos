using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PruebaTecnicaImaginemos.Application.Commands.Product.DeleteProduct;

public sealed record DeleteProductCommand(Guid id) : ICommand<bool>{}
