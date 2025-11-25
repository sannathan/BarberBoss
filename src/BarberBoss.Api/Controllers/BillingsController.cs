using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.GetById;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Application.UseCases.Billings.Update;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [ProducesResponseType(typeof(ResponseBillingsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllBillingsUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Billings.Count != 0)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById([FromServices] IGetBillingByIdUseCase useCase, [FromRoute] Guid Id)
        {
            var response = await useCase.Execute(Id);

            return Ok(response);
        }

        [HttpPatch("{Id}")]
        [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromServices] IUpdateBillingUseCase useCase, [FromRoute] Guid Id, [FromBody] RequestBillingJson request)
        {
            await useCase.Execute(Id, request);

            return NoContent();
        }

    }
}
