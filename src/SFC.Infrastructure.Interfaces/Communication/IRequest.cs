using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Interfaces.Communication
{

    public interface IRequest<TResponse> where TResponse : IResponse
    {
    }
}
