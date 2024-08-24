using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartments;

namespace Bookify.Application.SearchApartments;

public record SearchApartmentsQuery(DateOnly Start , DateOnly End): IQuery<IReadOnlyList<ApartmentResponse>>, IQuery<ApartmentResponse>;