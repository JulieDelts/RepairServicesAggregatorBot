using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class CurrentAdminOrdersMenuState: AbstractState
    {
        private List<InitialOrderOutputModel> _orders;

        private int _messageId;

        private int _counter;

        private OrderRepository _orderRepository;

        public CurrentAdminOrdersMenuState(int messageId, List<InitialOrderOutputModel> currentOrders)
        {
            _messageId = messageId;

            _orders = currentOrders;

            _counter = 0;

            _orderRepository = new();
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nxt")
            {
                if (_counter != _orders.Count - 1)
                {
                    _counter++;
                }
                else
                {
                    _counter = 0;
                }
            }
            else if (message.Data == "prv")
            {
                if (_counter != 0)
                {
                    _counter--;
                }
                else
                {
                    _counter = _orders.Count - 1;
                }

            }
            else if (message.Data == "bck")
            {
                context.State = new AdminOrdersMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_orders.Count == 0)
            {
                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Текущих заказов нет.", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else if (_orders.Count == 1)
            {
                var order = _orders[0];

                string orderInfo = SetOrderInfoByStatus(order);

                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                }); 

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, orderInfo, replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                var order = _orders[_counter];

                string orderInfo = SetOrderInfoByStatus(order);

                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("⬅️", "prv"),
                        InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                        InlineKeyboardButton.WithCallbackData("➡️", "nxt")
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, orderInfo, replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
        }

        private string SetOrderInfoByStatus(InitialOrderOutputModel order)
        {
            StringBuilder orderInfo = new($"Текущие заказы:\nID заказа: {order.Id}\nОписание: {order.OrderDescription}\nАдрес: {order.Address}\nДата создания: {order.Date}\nСтатус: {order.StatusDescription}\n");

            if (order.StatusId == 1)
            {
                var confirmedOrder = order as ConfirmedOrderOutputModel;

                orderInfo.Append($"Тип услуги: {confirmedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {confirmedOrder.AdminName}\n");
            }
            else if (order.StatusId == 2)
            {
                var unassignedOrder = order as UnassignedOrderOutputModel;

                orderInfo.Append($"Тип услуги: {unassignedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {unassignedOrder.AdminName}\n");
            }
            else if (order.StatusId == 3 || order.StatusId == 4)
            {
                var assignedOrder = order as AssignedOrderOutputModel;

                orderInfo.Append($"Тип услуги: {assignedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {assignedOrder.AdminName}\nСотрудник: {assignedOrder.ContractorName}\n Стоимость: {assignedOrder.Cost}\n");
            }

            return orderInfo.ToString();
        }
    }
}
