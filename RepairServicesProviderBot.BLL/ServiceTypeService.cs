using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

namespace RepairServicesProviderBot.BLL
{
    public class ServiceTypeService
    {
        ServiceTypeRepository ServiceTypeRepository {  get; set; }

        private Mapper _mapper;

        public ServiceTypeService()
        {
            ServiceTypeRepository = new();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new ServiceTypeMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public int AddServiceType(ServiceTypeInputModel serviceType)
        {
            var serviceTypeDescription = serviceType.ServiceTypeDescription;

            var serviceTypeId = ServiceTypeRepository.AddServiceType(serviceTypeDescription);

            return serviceTypeId;
        }

        public int AddContractorServiceType(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            int userId = ServiceTypeRepository.AddContractorServiceType(contractorServiceTypeDTO);

            return userId;
        }

        public ExtendedServiceTypeOutputModel GetServiceTypeById(int serviceTypeId)
        {
            var serviceTypeDTO = ServiceTypeRepository.GetServiceTypeById(serviceTypeId);

            var serviceTypeResponse = _mapper.Map<ExtendedServiceTypeOutputModel>(serviceTypeDTO);

            return serviceTypeResponse;
        }

        public ContractorServiceTypeOutputModel GetContractorServiceType(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            var contractorServiceTypeDTOResponse = ServiceTypeRepository.GetContractorServiceType(contractorServiceTypeDTO);

            var contractorServiceTypeResponse = _mapper.Map<ContractorServiceTypeOutputModel>(contractorServiceTypeDTOResponse);

            return contractorServiceTypeResponse;
        }

        public List<ExtendedServiceTypeOutputModel> GetAvailableServices()
        { 
            List<ServiceTypeDTO> serviceTypeDTOs = ServiceTypeRepository.GetAvailableServiceTypes();

            List<ExtendedServiceTypeOutputModel> serviceTypeOutputModels = new();

            foreach (var serviceTypeDTO in serviceTypeDTOs)
            {
                var serviceTypeOutput = _mapper.Map<ExtendedServiceTypeOutputModel>(serviceTypeDTO);

                serviceTypeOutputModels.Add(serviceTypeOutput);
            }

            return serviceTypeOutputModels;
        }

        public List<ContractorServiceTypeOutputModel> GetContractorServiceTypesById(int userId)
        {
            List<ServiceTypeDTO> contractorServiceTypeDTOs = ServiceTypeRepository.GetContractorServiceTypesById(userId);

            List<ContractorServiceTypeOutputModel> contractorServiceTypeOutputModels = new();

            foreach (var contractorServiceTypeDTO in contractorServiceTypeDTOs)
            {
                var contractorServiceTypeOutput = _mapper.Map<ContractorServiceTypeOutputModel>(contractorServiceTypeDTO);

                contractorServiceTypeOutputModels.Add(contractorServiceTypeOutput);
            }

            return contractorServiceTypeOutputModels;
        }

        public int UpdateServiceType(ExtendedServiceTypeInputModel serviceType)
        {
            var serviceTypeDTO = _mapper.Map<ServiceTypeDTO>(serviceType);

            int serviceTypeId = ServiceTypeRepository.UpdateServiceType(serviceTypeDTO);

            return serviceTypeId;
        }

        public int UpdateContractorServiceCost(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            int userId = ServiceTypeRepository.UpdateContractorServiceCost(contractorServiceTypeDTO);

            return userId;
        }

        public void HideServiceTypeById(int serviceTypeId)
        {
            ServiceTypeRepository.HideServiceTypeById(serviceTypeId);
        }

        public void DeleteContractorServiceType(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            ServiceTypeRepository.DeleteContractorServiceType(contractorServiceTypeDTO);
        }
    }
}
