using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

namespace RepairServicesProviderBot.BLL
{
    public class ContractorService
    {
        public UserRepository UserRepository { get; set; }

        private Mapper _mapper;

        public ContractorService()
        {
            UserRepository = new UserRepository();

            var config = new MapperConfiguration(
                cfg => {
                    cfg.AddProfile(new ContractorMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public double GetContractorRating(int userId)
        {
            double? rating = UserRepository.GetContractorRating(userId);

            return rating ?? 0;
        }

        public List<ExtendedContractorOutputModel> GetAllContractors()
        {
            var contractorDTOs = UserRepository.GetAllContractors();

            List<ExtendedContractorOutputModel> contractors = new List<ExtendedContractorOutputModel>();

            foreach (var contractorDTO in contractorDTOs)
            { 
                var contractor = _mapper.Map<ExtendedContractorOutputModel>(contractorDTO);

                contractors.Add(contractor);
            }

            return contractors;
        }
    }
}
