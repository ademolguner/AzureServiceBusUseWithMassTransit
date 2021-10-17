using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusExample.Api.Common;
using ServiceBusExample.Application.Business.Posts.Commands;
using System.Threading.Tasks;

namespace ServiceBusExample.Api.Controllers
{
    
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public class PostsController : ApiControllerBase
    {

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<ActionResult<CreatePostCommandOutput>> Create(
            [FromBody] CreatePostCommandInput createPostCommandInput)
        {
            var result = await Mediator.Send(createPostCommandInput);
            return Ok(result);
        }
    }
}
