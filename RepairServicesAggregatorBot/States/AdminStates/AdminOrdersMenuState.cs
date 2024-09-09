using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.BLL;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class AdminOrdersMenuState: AbstractState
    {
        private int _messageId;

        public AdminOrdersMenuState(int messageId)
        {
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nwordrs")
            {
                OrderService orderService = new OrderService();

                var newOrders = orderService.GetNewOrders();

                context.State = new NewOrdersMenuState(_messageId, newOrders);
            }
            else if (message.Data == "actvordrs")
            {

            }
            else if (message.Data == "ordrshstr")
            {
 
            }
            else if (message.Data == "bck")
            {
                context.State = new AdminMenuState(_messageId);
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
                    InlineKeyboardButton.WithCallbackData("Новые заказы", "nwordrs"),
                    InlineKeyboardButton.WithCallbackData("Активные заказы", "actvordrs"),

                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("История заказов", "ordrshstr"),
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                }
            });

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню заказов", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
