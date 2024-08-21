using Npgsql;
using Dapper;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core;
using RepairServicesProviderBot.DAL.Querries;

namespace RepairServicesProviderBot.DAL
{
    public class ReviewRepository
    {
        public int AddReview(ReviewDTO review)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ReviewQueries.AddReviewQuery;

                var args = new
                {
                    orderId = review.OrderId,
                    description = review.Description,
                    image = review.Image,
                    rating = review.Rating
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }
    }
}
