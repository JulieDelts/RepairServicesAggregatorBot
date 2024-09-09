using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingContractorServiceType;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class NewOrdersMenuState: AbstractState
    {
        private List<InitialOrderOutputModel> _newOrders;

        private int _messageId;

        private int _counter;

        public NewOrdersMenuState(int messageId, List<InitialOrderOutputModel> initialOrderOutputModels) 
        {
            _newOrders = initialOrderOutputModels;

            _messageId = messageId;

            _counter = 0;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nxt")
            {
                if (_counter != _newOrders.Count - 1)
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
                    _counter = _newOrders.Count - 1;
                }
            }
            else if (message.Data == "cnf")
            {
                
            }
            else if (message.Data == "dcl")
            {
                
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
            if (_newOrders.Count == 0)
            {
                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Новых заказов нет.", replyMarkup: keyboard);
            }
            else if (_newOrders.Count == 1)
            {

                var order = _newOrders[0];

                string  orderDescription = $"ID: {order.Id}\nОписание: {order.OrderDescription}\nДата создания: {order.Date}\nID клиента: {order.ClientId}\nИмя клиента: {order.ClientName}\nАдрес: {order.Address}";

                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Подтвердить", "cnf"),
                        InlineKeyboardButton.WithCallbackData("Отклонить", "dcl")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"Новые заказы:\n{orderDescription}", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                var order = _newOrders[_counter];

                string orderDescription = $"ID: {order.Id}\nОписание: {order.OrderDescription}\nДата создания: {order.Date}\nID клиента: {order.ClientId}\nИмя клиента: {order.ClientName}\nАдрес: {order.Address}";

                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Подтвердить", "cnf"),
                    InlineKeyboardButton.WithCallbackData("Отклонить", "dcl"),

                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("⬅️", "prv"),
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    InlineKeyboardButton.WithCallbackData("➡️", "nxt")
                }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"Новые заказы:\n{orderDescription}", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
        }
    }
}
