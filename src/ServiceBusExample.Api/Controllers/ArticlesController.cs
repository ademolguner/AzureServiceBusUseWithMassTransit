using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusExample.Api.Common;
using ServiceBusExample.Application.Business.Articles.Commands;
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
    public class ArticlesController : ApiControllerBase
    {

        [HttpPost]
        [Route("/api/[controller]")]
        public async Task<ActionResult<CreateArticleCommandOutput>> Create(
            [FromBody] CreateArticleCommandInput createArticleCommandInput)
        {
            var result = await Mediator.Send(createArticleCommandInput);
            return Ok(result);
        }
    }
}
