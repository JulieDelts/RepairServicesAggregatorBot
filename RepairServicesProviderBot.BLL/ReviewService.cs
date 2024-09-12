using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

namespace RepairServicesProviderBot.BLL
{
    public class ReviewService
    {
        ReviewRepository ReviewRepository { get; set; }

        private Mapper _mapper;

        public ReviewService()
        {
            ReviewRepository = new();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new ReviewMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public int AddReview(ReviewInputModel review)
        {
            var reviewDTO = _mapper.Map<ReviewDTO>(review);

            int orderId = ReviewRepository.AddReview(reviewDTO);

            return orderId;
        }

        public ReviewOutputModel GetReviewByOrderId(int orderId)
        {
            var reviewDTO = ReviewRepository.GetReviewByOrderId(orderId);

            var reviewResponse = _mapper.Map<ReviewOutputModel>(reviewDTO);

            return reviewResponse;
        }
    }
}
