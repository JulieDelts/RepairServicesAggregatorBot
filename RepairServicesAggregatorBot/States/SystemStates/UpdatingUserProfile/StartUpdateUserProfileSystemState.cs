using System.Security.Cryptography.X509Certificates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingUserProfile
{
    public class StartUpdateUserProfileSystemState : AbstractState
    {
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {

        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Обновление профиля клиента");
            
            UserInputModel client = new UserInputModel();

            context.State = new GetNameSystemState(client);

        }
    }
}
