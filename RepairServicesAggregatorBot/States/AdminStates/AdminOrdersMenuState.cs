using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesAggregatorBot.Bot.States.OrderStates.ShowOrderStates;

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
                OrderService orderService = new();

                var newOrders = orderService.GetNewOrders();

                context.State = new NewOrdersMenuState(_messageId, newOrders);
            }
            else if (message.Data == "actvordrs")
            {
                OrderService orderService = new();

                var clientOrders = orderService.GetAllOrdersByUserId(context.Id);

                List<InitialOrderOutputModel> currentOrders = new();

                foreach (var order in clientOrders)
                {
                    if (order.StatusId < 5 && order.IsDeleted == false)
                    {
                        currentOrders.Add(order);
                    }
                }

                context.State = new CurrentAdminOrdersMenuState(_messageId, currentOrders);
            }
            else if (message.Data == "ordrshstr")
            {
                OrderService orderService = new();

                var clientOrders = orderService.GetAllOrdersByUserId(context.Id);

                List<InitialOrderOutputModel> inactiveOrders = new();

                foreach (var order in clientOrders)
                {
                    if ((order.StatusId == 5 || order.StatusId == 6) && order.IsDeleted == false)
                    {
                        inactiveOrders.Add(order);
                    }
                }

                context.State = new OrderHistoryState(_messageId, inactiveOrders);
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
            InlineKeyboardMarkup keyboard = new(
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
