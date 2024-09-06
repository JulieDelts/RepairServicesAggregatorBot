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
            ServiceTypeRepository = new ServiceTypeRepository();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new ServiceTypeMapperProfile());
                });
            _mapper = new Mapper(config);
        }
        public ExtendedServiceTypeOutputModel AddServiceType(ServiceTypeInputModel serviceType)
        {
            var serviceTypeDescription = serviceType.ServiceTypeDescription;

            var serviceTypeId = ServiceTypeRepository.AddServiceType(serviceTypeDescription);

            var serviceTypeResponse = GetServiceTypeById(serviceTypeId);

            return serviceTypeResponse;
        }

        public ContractorServiceTypeOutputModel AddContractorServiceType(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            ServiceTypeRepository.AddContractorServiceType(contractorServiceTypeDTO);

            var contractorServiceTypeResponce = GetContractorServiceTypeById(contractorServiceType);

            return contractorServiceTypeResponce;
        }

        public ExtendedServiceTypeOutputModel GetServiceTypeById(int serviceTypeId)
        {
            var serviceTypeDTO = ServiceTypeRepository.GetServiceTypeById(serviceTypeId);

            var serviceTypeResponse = _mapper.Map<ExtendedServiceTypeOutputModel>(serviceTypeDTO);

            return serviceTypeResponse;
        }

        public ContractorServiceTypeOutputModel GetContractorServiceTypeById(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            var contractorServiceTypeDTOResponse = ServiceTypeRepository.GetContractorServiceTypeById(contractorServiceTypeDTO);

            var contractorServiceTypeResponse = _mapper.Map<ContractorServiceTypeOutputModel>(contractorServiceTypeDTOResponse);

            return contractorServiceTypeResponse;
        }

        public List<ExtendedServiceTypeOutputModel> GetAvailableServices()
        { 
            List<ServiceTypeDTO> serviceTypeDTOs = ServiceTypeRepository.GetAvailableServiceTypes();
            List<ExtendedServiceTypeOutputModel> serviceTypeOutputModels = new List<ExtendedServiceTypeOutputModel>();

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
            List<ContractorServiceTypeOutputModel> contractorServiceTypeOutputModels = new List<ContractorServiceTypeOutputModel>();

            foreach (var contractorServiceTypeDTO in contractorServiceTypeDTOs)
            {
                var contractorServiceTypeOutput = _mapper.Map<ContractorServiceTypeOutputModel>(contractorServiceTypeDTO);
                contractorServiceTypeOutputModels.Add(contractorServiceTypeOutput);
            }

            return contractorServiceTypeOutputModels;
        }

        public ExtendedServiceTypeOutputModel UpdateServiceTypeById(ExtendedServiceTypeInputModel serviceType)
        {
            var serviceTypeDTO = _mapper.Map<ServiceTypeDTO>(serviceType);

            int serviceTypeId = ServiceTypeRepository.UpdateServiceTypeById(serviceTypeDTO);

            var serviceTypeDTOResponse = ServiceTypeRepository.GetServiceTypeById(serviceTypeId);

            var serviceTypeResponse = _mapper.Map<ExtendedServiceTypeOutputModel>(serviceTypeDTOResponse);

            return serviceTypeResponse;
        }

        public ContractorServiceTypeOutputModel UpdateContractorServiceCostById(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            ServiceTypeRepository.UpdateContractorServiceCostById(contractorServiceTypeDTO);

            var contractorServiceTypeResponce = GetContractorServiceTypeById(contractorServiceType);

            return contractorServiceTypeResponce;
        }

        public void HideServiceTypeById(int serviceTypeId)
        {
            ServiceTypeRepository.HideServiceTypeById(serviceTypeId);
        }

        public void DeleteContractorServiceTypeById(ContractorServiceTypeInputModel contractorServiceType)
        {
            var contractorServiceTypeDTO = _mapper.Map<ServiceTypeDTO>(contractorServiceType);

            ServiceTypeRepository.DeleteContractorServiceTypeById(contractorServiceTypeDTO);
        }
    }
}
