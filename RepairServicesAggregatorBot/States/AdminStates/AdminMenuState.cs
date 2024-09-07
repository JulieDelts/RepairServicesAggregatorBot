using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class AdminMenuState : AbstractState
    {
        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "prf")
            {
                context.State = new UserProfileMenuState();
            }
            else if (message.Data == "srvtp")
            {
                context.State = new AdminServiceTypeMenuState();
            }
            else if (message.Data == "cntrctr")
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
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Меню услуг", "srvtp"),
                    InlineKeyboardButton.WithCallbackData("Меню сотрудников", "cntrctr"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Меню заказов", "cntrctr"),
                    InlineKeyboardButton.WithCallbackData("Меню клиентов", "srvs")
                },
                new[]
                {  
                    InlineKeyboardButton.WithCallbackData("Профиль", "prf"),
                    InlineKeyboardButton.WithCallbackData("Статистика", "ststcs"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Модификация пользователя", "usrmdf")
                }
            });

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Меню администратора", replyMarkup: keyboard);
        }
    }
}
