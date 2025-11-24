using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
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
        [ProducesResponseType(typeof(ResponseRegisterBillingJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterBillingUseCase useCase, [FromBody] RequestBillingJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
