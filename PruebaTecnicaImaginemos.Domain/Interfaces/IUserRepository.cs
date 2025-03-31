using PruebaTecnicaImaginemos.Domain.DTOs.User;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Interfaces;

public interface IUserRepository
{
    Task<UserE> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserDTO>> GetUsers();

    void Delete(UserE entity);
}
