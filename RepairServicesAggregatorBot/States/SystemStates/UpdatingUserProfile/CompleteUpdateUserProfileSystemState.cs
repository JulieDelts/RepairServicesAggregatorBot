using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingUserProfile
{
    public class CompleteUpdateUserProfileSystemState: AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        public CompleteUpdateUserProfileSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            UserService userService = new UserService();

            var client = userService.GetUserById(context.Id);

            var updatedClient = new ExtendedUserInputModel() 
            {
                Id = context.Id,
                Name = UserInputModel.Name,
                Email = UserInputModel.Email,
                Phone = UserInputModel.Phone,
                ChatId = client.ChatId,
                RoleId = client.RoleId,
                IsDeleted = client.IsDeleted,
                Image = client.Image
            };

            userService.UpdateUserById(updatedClient);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Обновление профиля завершено.");

            context.State = new ClientProfileMenuState();
        }
    }
}
