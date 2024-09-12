using AutoMapper;
using RepairServicesProviderBot.BLL.Mappings;
using RepairServicesProviderBot.Core.DTOs;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;

namespace RepairServicesProviderBot.BLL
{
    public class UserService
    {
        public UserRepository UserRepository { get; set; }

        private Mapper _mapper;

        public UserService()
        {
            UserRepository = new();

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new UserMapperProfile());
                });
            _mapper = new Mapper(config);
        }

        public int AddUser(UserInputModel user)
        {
            var userDTO = _mapper.Map<UserDTO>(user);

            var userId = UserRepository.AddUser(userDTO);

            return userId;
        }

        public ExtendedUserOutputModel GetUserById(int userId)
        {
            var userDTO = UserRepository.GetUserById(userId);

            var userResponse = _mapper.Map<ExtendedUserOutputModel>(userDTO);

            return userResponse;
        }

        public ExtendedUserOutputModel GetUserByChatId(long chatId)
        {
            var userDTO = UserRepository.GetUserByChatId(chatId);

            var userResponse = _mapper.Map<ExtendedUserOutputModel>(userDTO);

            return userResponse;
        }

        public int UpdateUser(ExtendedUserInputModel user)
        {
            var userDTO = _mapper.Map<UserDTO>(user);

            int userId = UserRepository.UpdateUser(userDTO);

            return userId;
        }

        public int HideUserById(int userId)
        {
            int user = UserRepository.HideUserById(userId);

            return user;
        }
    }
}
