﻿using RepairServicesAggregatorBot.Bot.States.OrderStates.ManageExistOrderStates;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.DAL;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ClientStates
{
    public class CurrentClientOrdersMenu : AbstractState
    {
        private List<InitialOrderOutputModel> _orders;

        private int _messageId;

        private int _counter;

        private OrderRepository _orderRepository;

        public CurrentClientOrdersMenu(int messageId, List<InitialOrderOutputModel> currentOrders)
        {
            _messageId = messageId;
            _orders = currentOrders;
            _counter = 0;
            _orderRepository = new OrderRepository();
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
                context.State = new ClientOrdersMenuState(_messageId);
            }
            else if (message.Data.StartsWith("cncl"))
            {
                var orderId = Convert.ToInt32(message.Data.Split("cncl")[1]);

                context.State = new CancelOrderState(orderId);
            }
            else if (message.Data.StartsWith("cntrctr"))//ZABRAL
            {
                var orderId = Convert.ToInt32(message.Data.Split("cntrctr")[1]);
            }
            else if (message.Data.StartsWith("cmpltordr"))//ZABRAL
            {
                var orderId = Convert.ToInt32(message.Data.Split("cmpltordr")[1]);

            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var order = _orders[_counter];

            string orderInfo = $"Текущие заказы:\nID заказа: {order.Id}\nОписание: {order.OrderDescription}\nАдрес: {order.Address}\nДата создания: {order.Date}\nСтатус: {order.StatusDescription}\n";

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Отменить заказ", $"cncl{order.Id}")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("⬅️", "prv"),
                        InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                        InlineKeyboardButton.WithCallbackData("➡️", "nxt")
                    }
                });;

            if (order.StatusId == 1)
            {
                var confirmedOrder = order as ConfirmedOrderOutputModel;

                orderInfo += $"Тип услуги: {confirmedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {confirmedOrder.AdminName}\n";
            }
            else if (order.StatusId == 2)
            {
                var unassignedOrder = order as UnassignedOrderOutputModel;

                orderInfo += $"Тип услуги: {unassignedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {unassignedOrder.AdminName}\n";

                keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Выбор сотрудника", $"cntrctr{unassignedOrder.Id}"),
                        InlineKeyboardButton.WithCallbackData("Отменить заказ", $"cncl{unassignedOrder.Id}")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("⬅️", "prv"),
                        InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                        InlineKeyboardButton.WithCallbackData("➡️", "nxt")
                    }
               });
            }
            else if (order.StatusId == 3)
            {
                var assignedOrder = order as AssignedOrderOutputModel;

                orderInfo += $"Тип услуги: {assignedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {assignedOrder.AdminName}\nСотрудник: {assignedOrder.ContractorName}\n Стоимость: {assignedOrder.Cost}\n";
            }
            else if (order.StatusId == 4)
            {
                var assignedOrder = order as AssignedOrderOutputModel;

                orderInfo += $"Тип услуги: {assignedOrder.ServiceType.ServiceTypeDescription}\nАдминистратор: {assignedOrder.AdminName}\nСотрудник: {assignedOrder.ContractorName}\n Стоимость: {assignedOrder.Cost}\n";

                keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Пометить заказ как выполненный", $"cmpltordr{assignedOrder.Id}"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Отменить заказ", $"cncl{assignedOrder.Id}")
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

