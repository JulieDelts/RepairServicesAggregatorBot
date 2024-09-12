using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class CurrentContractorOrdersMenuState: AbstractState
    {
        private List<InitialOrderOutputModel> _orders;

        private int _messageId;

        private int _counter;

        private OrderRepository _orderRepository;

        public CurrentContractorOrdersMenuState(int messageId, List<InitialOrderOutputModel> currentOrders)
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
                context.State = new ContractorOrdersMenuState(_messageId);
            }
            else if (message.Data == "exec")
            {
                OrderService orderService = new();

                var order = orderService.GetOrderSystemInfoById(_orders[_counter].Id);

                var updatedOrder = new ExtendedOrderInputModel()
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    ContractorId = order.ContractorId,
                    AdminId = order.AdminId,
                    StatusId = 4,
                    ServiceTypeId = order.ServiceTypeId,
                    Date = order.Date,
                    OrderDescription = order.OrderDescription,
                    Address = order.Address,
                    IsDeleted = order.IsDeleted
                };

                context.State = new ExecuteOrderContractorState(_messageId, updatedOrder);
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

                var assignedOrder = order as AssignedOrderOutputModel;

                string orderInfo = $"Текущие заказы:\nID заказа: {assignedOrder.Id}\nОписание: {assignedOrder.OrderDescription}\nАдрес: {assignedOrder.Address}\nДата создания: {assignedOrder.Date}\nСтатус: {assignedOrder.StatusDescription}\nТип услуги: {assignedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {assignedOrder.AdminName}\nСотрудник: {assignedOrder.ContractorName}\n Стоимость: {assignedOrder.Cost}\n";

                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                });

                if (order.StatusId == 3)
                {
                    keyboard = new(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Перейти к выполнению", "exec"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                        }
                    });
                }

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, orderInfo, replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                var order = _orders[_counter];

                var assignedOrder = order as AssignedOrderOutputModel;

                string orderInfo = $"Текущие заказы:\nID заказа: {assignedOrder.Id}\nОписание: {assignedOrder.OrderDescription}\nАдрес: {assignedOrder.Address}\nДата создания: {assignedOrder.Date}\nСтатус: {assignedOrder.StatusDescription}\nТип услуги: {assignedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {assignedOrder.AdminName}\nСотрудник: {assignedOrder.ContractorName}\n Стоимость: {assignedOrder.Cost}\n";

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

                if (order.StatusId == 3)
                {
                    keyboard = new(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Перейти к выполнению", "exec"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⬅️", "prv"),
                            InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                            InlineKeyboardButton.WithCallbackData("➡️", "nxt")
                        }
                    });
                }

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, orderInfo, replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
        }
    }
}

