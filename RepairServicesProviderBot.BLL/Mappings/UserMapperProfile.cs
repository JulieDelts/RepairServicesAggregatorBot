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
    public class UserMapperProfile: Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<ExtendedUserInputModel, UserDTO>();
            CreateMap<UserDTO,ExtendedUserOutputModel>();
        }
    }
}
