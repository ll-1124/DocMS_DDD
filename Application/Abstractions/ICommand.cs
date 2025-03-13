using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ICommand : IRequest<Unit> { }

    public interface ICommand<TResponse> : IRequest<TResponse> { }
}
