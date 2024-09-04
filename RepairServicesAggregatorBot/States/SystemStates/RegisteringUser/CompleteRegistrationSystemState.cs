using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser
{
    public class CompleteRegistrationSystemState : AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        public CompleteRegistrationSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
        }

        public override void HandleMessage(Context context, Update update)
        {
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            UserService adminService = new UserService();
            int qwe = adminService.AddUser(UserInputModel);
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Регистрация завершена.");

            if (UserInputModel.RoleId == 1)
            {
                context.State = new ClientMenuState(UserInputModel);
            }
            else if (UserInputModel.RoleId == 2)
            {
                //context.State = new ContractorMenuState(UserInputModel);
            }
            else
            {
                // context.State = new AdminMenuState(UserInputModel);
            }
        }
    }
}
