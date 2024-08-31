using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.Bookings.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Bookings
{
    [ApiController]
    [Route("api/bookings")]
    public class Bookings : ControllerBase
    {
        private readonly ISender _sender;

        public Bookings(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooking(Guid id , CancellationToken cancellationToken)
        {
            var query = new GetBookingQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        public async Task<IActionResult> ReserveBooking(ReserveBookingRequest request,
            CancellationToken cancellationToken)
        {
            var command =  new ReserveBookingCommand(request.UserId,
                request.ApartmentId,
                request.StartDate,
                request.EndDate);


            var result = await _sender.Send(command, cancellationToken);
            
            return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
        }
    }
}
