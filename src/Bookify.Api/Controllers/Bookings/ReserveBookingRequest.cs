namespace Bookify.Api.Controllers.Bookings;

public record ReserveBookingRequest(Guid UserId,Guid ApartmentId,DateOnly StartDate,DateOnly EndDate);