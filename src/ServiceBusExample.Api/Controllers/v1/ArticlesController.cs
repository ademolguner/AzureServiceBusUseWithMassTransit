using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusExample.Api.Common;
using ServiceBusExample.Application.Business.Articles.Commands;
using System.Threading.Tasks;

namespace ServiceBusExample.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("/api/[controller]")] 
    public class ArticlesController : ApiControllerBase
    {
        
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CreateArticleCommandOutput), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateArticleCommandOutput>> CreateAsync(
            [FromBody] CreateArticleCommandInput createArticleCommandInput)
        {
            var result = await Mediator.Send(createArticleCommandInput);
            return Ok(result);
        }
    }
}
