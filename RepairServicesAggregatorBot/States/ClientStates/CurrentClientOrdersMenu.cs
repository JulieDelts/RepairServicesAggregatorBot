using RepairServicesProviderBot.Core.OutputModels;
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

        public CurrentClientOrdersMenu(int messageId, List<InitialOrderOutputModel> currentOrders)
        {
            _messageId = messageId;
            _orders = currentOrders;
            _counter = 0;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
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
            else if (message.Data == "cncl")
            {
                //cancel order menu 
            }
            else if (message.Data == "cntrctr")
            {
                //choose cnrtct 
            }
            else if (message.Data == "cmpltordr")
            {
                //completeorder
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
                        InlineKeyboardButton.WithCallbackData("Отменить заказ", "cncl")
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
                        InlineKeyboardButton.WithCallbackData("Выбор сотрудника", "cntrctr"),
                        InlineKeyboardButton.WithCallbackData("Отменить заказ", "cncl")
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
                        InlineKeyboardButton.WithCallbackData("Пометить заказ как выполненный", "cmpltordr"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Отменить заказ", "cncl")
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

