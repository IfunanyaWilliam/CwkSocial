using AutoMapper;
using CwkSocial.Api.Contracts.UserProfile.Requests;
using CwkSocial.Api.Contracts.UserProfile.Responses;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.Application.UserProfiles.Queries;
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
        private readonly IMapper _mapper;

        public UserProfileController(IMediator mediator,
                                    IMapper mapper)
        {
            _mediator   = mediator;
            _mapper     = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query       = new GetAllUserProfiles();
            var response    = await _mediator.Send(query);
            var profiles    = _mapper.Map<List<UserProfileResponse>>(response);

            return Ok(profiles);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserProfileCreate([FromBody] UserProfileCreateUpdate profile)
        {
            var command     = _mapper.Map<CreateUserCommand>(profile);
            var response    = await _mediator.Send(command);
            var userProfile = _mapper.Map<UserProfileResponse>(response);

            return CreatedAtAction(nameof(GetUserProfileById), new { id = response.UserProfileId }, userProfile);
        }

        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [HttpGet]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var query       = new GetUserProfileById { UserProfileId = Guid.Parse(id) };
            var response    = await _mediator.Send(query);
            var userProfile = _mapper.Map<UserProfileResponse>(response);

            return Ok(userProfile);
        }


        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [HttpPatch]
        public async Task<IActionResult> UpdateUserProfile(string id, UserProfileCreateUpdate updateProfile)
        {
            var command             = _mapper.Map<UpdateUserProfileBasicInfo>(updateProfile);
            command.UserProfileId   = Guid.Parse(id);
            var responsse = await _mediator.Send(command);

            return NoContent();
        }


        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var command = new DeleteUserProfile { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(command);

            return NoContent();
        }
    }
}

