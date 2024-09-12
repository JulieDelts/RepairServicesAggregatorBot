using AutoMapper;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesProviderBot.BLL.Mappings
{
    public class ReviewMapperProfile: Profile
    {
        public ReviewMapperProfile() 
        {
            CreateMap<ReviewInputModel, ReviewDTO>();
            CreateMap<ReviewDTO, ReviewOutputModel>();
        }
    }
}
