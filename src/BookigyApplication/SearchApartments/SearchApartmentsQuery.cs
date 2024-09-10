using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartments;

namespace Bookify.Application.SearchApartments;

public record SearchApartmentsQuery(DateOnly StartDate , DateOnly EndDate): IQuery<IReadOnlyList<ApartmentResponse>>;