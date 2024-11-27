using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Application.SearchApartments;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Apartments;

public class SearchApartmentsQueryHandler:IQueryHandler<SearchApartmentsQuery,IReadOnlyList<ApartmentResponse>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    private static readonly int[] ActiveBookingStatuses =
    {
            (int)BookingStatus.Confirmed
            ,(int)BookingStatus.Completed
            ,(int)BookingStatus.Reserved

    };

    public SearchApartmentsQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }


    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        //Check for OverLapping
        if (request.StartDate > request.EndDate)
        {
            return new List<ApartmentResponse>();
        }

        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
                           SELECT
                               a.id AS Id,
                               a.name AS Name,
                               a.description AS Description,
                               a.price_amount AS Price,
                               a.price_currency AS Currency,
                               a.address_country AS Country,
                               a.address_state AS State,
                               a.address_zip_code AS ZipCode,
                               a.address_city AS City,
                               a.address_street AS Street
                           FROM apartments AS a
                           WHERE NOT EXISTS
                           (
                               SELECT 1
                               FROM bookings AS b
                               WHERE
                                   b.apartment_id = a.id AND
                                   b.duration_start <= @EndDate AND
                                   b.duration_end >= @StartDate AND
                                   b.booking_status = ANY(@ActiveBookingStatuses)
                           )
                           """;

        var apartment = await connection.QueryAsync<
            ApartmentResponse, AddressResponse, ApartmentResponse>(sql, 
            (apartment, address) =>
            {
                apartment.Address =address;
                return apartment;
            }
            ,new
            {
                request.StartDate,
                request.EndDate,
                ActiveBookingStatuses
            },
            //Splits Query On Country Column 
            splitOn: "Country"


        );
        return apartment.ToList();

    }
}