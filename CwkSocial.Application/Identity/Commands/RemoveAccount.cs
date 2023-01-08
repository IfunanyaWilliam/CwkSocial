

using CwkSocial.Application.Models;
using MediatR;

namespace CwkSocial.Application.Identity.Commands
{
    public class RemoveAccount : IRequest<OperationResult<bool>>
    {
        public Guid IdentityUserId { get; set; }
        public Guid RequesterGuid { get; set; }
    }
}
