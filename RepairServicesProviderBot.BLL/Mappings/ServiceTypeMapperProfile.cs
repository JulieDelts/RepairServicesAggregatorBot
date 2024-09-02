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
    public class ServiceTypeMapperProfile:Profile
    {
        public ServiceTypeMapperProfile()
        {
            CreateMap<ServiceTypeInputModel, ServiceTypeDTO>();
            CreateMap<ContractorServiceTypeInputModel, ServiceTypeDTO>();
            CreateMap<ExtendedServiceTypeInputModel, ServiceTypeDTO>();
            CreateMap<ServiceTypeDTO, ServiceTypeOutputModel>();
            CreateMap<ServiceTypeDTO, ContractorServiceTypeOutputModel>();
        }
    }
}
