using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class AdminContractorsMenuState: AbstractState
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
                context.State = new AdminMenuState();
            }
            else if (message.Data == "cntrctr")
            {
                context.State = new GetContractorState();
            }
            else if (message.Data == "allcntrctrs")
            {
                context.State = new AllContractorsState();
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
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

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Сотрудники:", replyMarkup: keyboard);
        }
    }
}
