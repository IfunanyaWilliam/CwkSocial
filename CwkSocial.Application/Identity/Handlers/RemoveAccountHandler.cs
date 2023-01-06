

using CwkSocial.Application.Identity.Commands;
using CwkSocial.Application.Models;
using MediatR;

namespace CwkSocial.Application.Identity.Handlers
{
    internal class RemoveAccountHandler : IRequestHandler<RemoveAccount, OperationResult<bool>>
    {
        public Task<OperationResult<bool>> Handle(RemoveAccount request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
