using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.AdminStates;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ShowOrderStates
{
    public class OrderHistoryState : AbstractState
    {
        private List<InitialOrderOutputModel> _orders;

        private int _messageId;

        private int _start;

        private int _end;

        public OrderHistoryState(int messageId, List<InitialOrderOutputModel> orders)
        {
            _messageId = messageId;
            _orders = orders;

            if (_orders.Count < 3)
            {
                _start = 0;
                _end = _orders.Count - 1;
            }
            else
            {
                _start = 0;
                _end = 2;
            }
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nxt")
            {
                if (_end == _orders.Count - 1)
                {
                    _start = 0;
                    _end = 2;
                }
                else if (_end + 3 <= _orders.Count - 1)
                {
                    _end += 3;
                    _start += 3;
                }
                else if (_end + 3 > _orders.Count - 1)
                {
                    _end = _orders.Count - 1;
                    _start += 3;
                }
            }
            else if (message.Data == "prv")
            {
                if (_start == 0)
                {
                    _end = _orders.Count - 1;

                    if (_orders.Count % 3 == 0)
                    {
                        _start = _end - 2;
                    }
                    else
                    {
                        _start = _orders.Count - _orders.Count % 3;
                    }
                }
                else if (_end - _start == 2)
                {
                    _end -= 3;
                    _start -= 3;
                }
                else
                {
                    _end = _start - 1;
                    _start = _end - 2;
                }
            }
            else if (message.Data == "bck")
            {
                if (context.RoleId == 1)
                {
                    context.State = new ClientOrdersMenuState(_messageId);
                }
                else if (context.RoleId == 2)
                {
                    //context.State = new ContractorOrdersMenuState(_messageId);
                }
                else if (context.RoleId == 3)
                {
                    context.State = new AdminOrdersMenuState(_messageId);
                }
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

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Истории заказов нет.", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else if (_orders.Count <= 3)
            {
                StringBuilder orderInfo = new("История заказов:\n");

                for (int i = _start; i <= _end; i++)
                {
                    orderInfo.Append($"\nID заказа: {_orders[i].Id}\nОписание: {_orders[i].OrderDescription}\nСтатус: {_orders[i].StatusDescription}");
                }

                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, orderInfo.ToString(), replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                StringBuilder orderInfo = new("История заказов:\n");

                for (int i = _start; i <= _end; i++)
                {
                    orderInfo.Append($"ID заказа: {_orders[i].Id}\nОписание: {_orders[i].OrderDescription}\nСтатус: {_orders[i].StatusDescription}");
                }

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

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, orderInfo.ToString(), replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
        }
    }
}
