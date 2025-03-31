using Microsoft.EntityFrameworkCore;
using PruebaTecnicaImaginemos.Application.Commands.User;
using PruebaTecnicaImaginemos.Domain.DTOs.User;
using PruebaTecnicaImaginemos.Domain.Interfaces;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Repository.EntityRepository;

internal class UserRepository : Repository<UserE>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
        var result = await DbContext.Users
                .Select(u => new UserDTO
                {
                    IdUser = u.Id,
                    Name = u.Name.value,
                    DNI = u.DNI.dni
                }).ToListAsync();

        return result;
    }
}
