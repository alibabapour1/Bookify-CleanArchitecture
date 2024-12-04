using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Reviews;
using Dapper;

namespace Bookify.Application.Reviews.GetReviews;

public class GetReviewsQueryHandler:IQueryHandler<GetReviewsQuery, IReadOnlyList<ReviewResponse>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetReviewsQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IReadOnlyList<ReviewResponse>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        using var connection =  _connectionFactory.CreateConnection();
        const string sql ="""
                          SELECT 
                                id As Id,
                                booking_id AS BookingId,
                                apartment_id AS ApartmentId,
                                comment AS Comment,
                                rating AS Rating
                          FROM reviews
                          WHERE apartment_id = @ApartmentId
                          """;

        var reviews = await connection.QueryAsync<ReviewResponse>(sql
            , new { request.ApartmentId });

        var reviewResponses = reviews.ToList();
        if (reviewResponses.Any() == false)
            return Result.Failure<IReadOnlyList<ReviewResponse>>(ReviewErrors.NotFound);

        return reviewResponses;
    }
}