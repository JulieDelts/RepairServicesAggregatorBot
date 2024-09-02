using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
