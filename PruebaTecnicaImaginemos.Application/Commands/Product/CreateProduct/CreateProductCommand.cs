using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Product;
using PruebaTecnicaImaginemos.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PruebaTecnicaImaginemos.Application.Commands.Product.CreateProduct;

public sealed record CreateProductCommand(string nameProd, int price, string description) : ICommand<Guid>;

