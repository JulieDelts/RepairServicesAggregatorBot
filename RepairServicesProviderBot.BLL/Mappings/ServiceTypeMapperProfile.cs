﻿using AutoMapper;
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
            CreateMap<ServiceTypeDTO, ExtendedServiceTypeOutputModel>();
            CreateMap<ServiceTypeDTO, ContractorServiceTypeOutputModel>();
        }
    }
}
