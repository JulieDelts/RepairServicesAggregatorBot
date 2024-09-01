using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates
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
            ClientService clientService = new ClientService();
            int qwe = clientService.AddClient(UserInputModel);
            Console.WriteLine(qwe);
            context.State = new StartRegistrationSystemState();
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Регистрация завершена.");
        }
    }
}
