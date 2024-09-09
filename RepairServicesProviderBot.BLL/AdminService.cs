using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

namespace RepairServicesProviderBot.BLL
{
    public class AdminService
    {
        public UserRepository UserRepository { get; set; }

        private Mapper _mapper;

        public AdminService()
        {
            UserRepository = new UserRepository();

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new AdminMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public List<AdminOutputModel> GeAllAdmins()
        {
            var adminDTOs = UserRepository.GetAllAdmins();

            List<AdminOutputModel> admins = new List<AdminOutputModel>();

            foreach (var adminDTO in adminDTOs)
            {
                var admin = _mapper.Map<AdminOutputModel>(adminDTO);
                admins.Add(admin);
            }

            return admins;
        }
    }
}
