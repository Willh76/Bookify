using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bookify.Application.Apartments.SearchApartments
{
    internal sealed class SearchApartmentQueryHandler : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
    {
        private static readonly int[] ActiveBookingStatuses =
        {
            (int)BookingStatus.Reserved,
            (int)BookingStatus.Confirmed,
            (int)BookingStatus.Completed
        };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SearchApartmentQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
        {
            if (request.StartDate > request.EndDate)
                return new List<ApartmentResponse>();

            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

            const string sql = """
                SELECT * FROM Apartment
                """;

            var apartments = await connection
                .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                sql,
                (apartment, address) =>
                {
                    apartment.Address = address;

                    return apartment;
                },
                new
                {
                    request.StartDate,
                    request.EndDate,
                    ActiveBookingStatuses
                },
                splitOn: "Country");

            return apartments.ToList();
        }
    }
}
