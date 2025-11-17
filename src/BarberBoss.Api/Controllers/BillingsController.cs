using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BarberBoss.API.Controllers
{
    [Route("api/billings")]
    [ApiController]
    public class BillingsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromServices] IRegisterBillingUseCase useCase, [FromBody] RequestBillingJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
