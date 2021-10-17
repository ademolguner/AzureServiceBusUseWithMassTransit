using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusExample.Api.Common;
using ServiceBusExample.Application.Business.Categories.Commands;
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
    public class CategoriesController : ApiControllerBase
    {
        
        [HttpDelete()]
        [Route("/api/[controller]/{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await Mediator.Send(new DeleteCategoryCommandInput { Id = id });
            return Ok(result);
        }
    }
}
