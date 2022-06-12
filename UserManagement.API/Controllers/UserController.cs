using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.API.Commands;
using UserManagement.API.Models;
using UserManagement.API.Models.Core;
using UserManagement.API.Queries;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Retrieves all users
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserView>>> GetUsers([FromQuery] GetUsersQuery query)
            => await ExecuteQuery(query);

        /// <summary>
        /// Retrieves a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserView>> GetUser([FromRoute] long id)
            => await ExecuteQuery(new GetUserQuery(id));

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IdentityResponse>> Create([FromBody] CreateUserCommand command)
            => await ExecuteCommand(command);

        /// <summary>
        /// Edits a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<IdentityResponse>> Create([FromRoute] long id, [FromBody] EditUserCommand command)
            => await ExecuteCommand(command);

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
            => await ExecuteCommand(new DeleteUserCommand(id));
    }
}
