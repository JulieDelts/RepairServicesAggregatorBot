using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesProviderBot.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class AllContractorsState : AbstractState
    {
        private int _messageId;

        public AllContractorsState(int messageId) 
        {
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new AdminContractorsMenuState(_messageId);
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

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, contractorsDescription, replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
