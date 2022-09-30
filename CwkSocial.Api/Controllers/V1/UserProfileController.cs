using CwkSocial.Api.Contracts.UserProfile.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CwkSocial.Api.Controllers.V1
{

    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfileController : Controller
    {
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            return (IActionResult)Task.FromResult(Ok());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserProfileCreate([FromBody] UserProfileCreate profile)
        {
            return (IActionResult)Task.FromResult(Ok());
        }
    }
}

