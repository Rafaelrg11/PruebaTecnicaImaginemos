using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.DTOs.Responses;

public class ResponseStandar<T>
{
    public T data { get; set; }
    public long total { get; set; }
}
