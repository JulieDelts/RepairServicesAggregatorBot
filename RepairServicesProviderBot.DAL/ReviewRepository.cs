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
                string query = ReviewQueries.AddReviewByOrderIdQuery;

                var args = new
                {
                    orderId = review.OrderId,
                    description = review.ReviewDescription,
                    rating = review.Rating
                };

                connection.Open();

                return connection.QuerySingle<int>(query, args);
            }
        }

        public ReviewDTO GetReviewByOrderId(int orderId)
        {
            string conectionString = Options.ConnectionString;

            using (var connection = new NpgsqlConnection(conectionString))
            {
                string query = ReviewQueries.GetReviewByOrderIdQuery;

                var args = new
                {
                    orderId = orderId
                };

                connection.Open();

                return connection.Query<ReviewDTO>(query, args).FirstOrDefault();
            }
        }
    }
}
