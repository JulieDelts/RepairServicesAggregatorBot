using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class AdminContractorsMenuState: AbstractState
    {
        private int _messageId;

        public AdminContractorsMenuState(int messageId)
        {
            _messageId = messageId;
        }
        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery; 

            if (message.Data == "bck")
            {
                context.State = new AdminMenuState(_messageId);
            }
            else if (message.Data == "cntrctr")
            {
                context.State = new GetContractorState(_messageId);
            }
            else if (message.Data == "allcntrctrs")
            {
                context.State = new AllContractorsState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Сотрудники сервиса", "allcntrctrs"),
                    InlineKeyboardButton.WithCallbackData("Профиль сотрудника", "cntrctr")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                }

            });

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню сотрудников", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
