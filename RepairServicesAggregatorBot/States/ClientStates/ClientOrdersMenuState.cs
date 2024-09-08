using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.ClientStates
{
    public class ClientOrdersMenuState: AbstractState
    {
        private int _messageId;

        public ClientOrdersMenuState(int messageId)
        {
            _messageId = messageId;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "crntordrs")
            {
                //context.State = new ClientMenuState(_messageId);
            }
            else if (message.Data == "bck")
            {
                context.State = new ClientMenuState(_messageId);
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
                    InlineKeyboardButton.WithCallbackData("Текущие заказы", "crntordrs"),
                    InlineKeyboardButton.WithCallbackData("История заказов", "ordrshstr"),

                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                }
            });

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню заказов пользователя", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
