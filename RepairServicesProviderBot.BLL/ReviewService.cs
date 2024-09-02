using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ReviewRepository = new ReviewRepository();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new ReviewMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public ReviewOutputModel AddReview(ReviewInputModel review)
        {
            var reviewDTO = _mapper.Map<ReviewDTO>(review);

            int orderId = ReviewRepository.AddReview(reviewDTO);

            var reviewResponse = GetReviewByOrderId(orderId);

            return reviewResponse;
        }

        public ReviewOutputModel GetReviewByOrderId(int orderId)
        {
            var reviewDTO = ReviewRepository.GetReviewByOrderId(orderId);

            var reviewResponse = _mapper.Map<ReviewOutputModel>(reviewDTO);

            return reviewResponse;
        }
    }
}
