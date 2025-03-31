using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.User;
using PruebaTecnicaImaginemos.Domain.Sale;
using PruebaTecnicaImaginemos.Domain.Users.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Users;

public sealed class UserE : Entity
{
    public UserE( Guid guid,
        Name name,
        DNI dni)
        : base (guid)
    {
        Name = name;
        DNI = dni;
    }
    private UserE() { }

    public Name Name { get; private set; }
    public DNI DNI { get; private set; }
    public ICollection<Sales> Sale { get; private set; } = new List<Sales>();

    public static UserE create(string name, string dni) 
    {
        var user = new UserE(Guid.NewGuid(), new Name(name) , new DNI(dni));

        user.RaiseDomainEvent(new UserCreateDomainEvent(user.Id));

        return user;
    }

    public void Update(UserDTO2 dto)
    {
        Name = new Name(dto.Name);
        DNI = new DNI(dto.DNI);
    }
}
