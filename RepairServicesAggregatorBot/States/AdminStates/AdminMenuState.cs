using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class AdminMenuState : AbstractState
    {
        private int _messageId;

        public AdminMenuState(int messageId = 0)
        {
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "prf")
            {
                context.State = new UserProfileMenuState(_messageId);
            }
            else if (message.Data == "ordrs")
            {
                context.State = new AdminOrdersMenuState(_messageId);
            }
            else if (message.Data == "srvtp")
            {
                context.State = new AdminServiceTypeMenuState(_messageId);
            }
            else if (message.Data == "cntrctr")
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
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Услуги", "srvtp"),
                    InlineKeyboardButton.WithCallbackData("Сотрудники", "cntrctr"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Заказы", "ordrs"),
                    InlineKeyboardButton.WithCallbackData("Клиенты", "clnts")
                },
                new[]
                {  
                    InlineKeyboardButton.WithCallbackData("Профиль", "prf")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Модификация пользователя", "usrmdf")
                }
            });

            if (_messageId == 0)
            {
                var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Меню администратора", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню администратора", replyMarkup: keyboard);
            }
        }
    }
}
