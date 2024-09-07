using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesProviderBot.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class AllContractorsState : AbstractState
    {
        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new AdminContractorsMenuState();
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ContractorService contractorService = new ContractorService();

            var contractors = contractorService.GetAllContractors();

            string contractorsDescription = "Сотрудники:\n";

            for (int i = 0; i < contractors.Count; i++)
            {
                contractorsDescription += $"{i + 1}. {contractors[i].Name}\nID: {contractors[i].Id}\nСкрыт: {contractors[i].IsDeleted}\n";
            }

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), contractorsDescription, replyMarkup: keyboard);
        }
    }
}
