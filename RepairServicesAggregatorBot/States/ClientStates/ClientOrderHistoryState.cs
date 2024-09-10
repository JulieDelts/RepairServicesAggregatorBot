using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ClientStates
{
    public class ClientOrderHistoryState : AbstractState
    {
        private List<InitialOrderOutputModel> _orders;

        private int _messageId;

        private int _start;

        private int _end;

        public ClientOrderHistoryState(int messageId, List<InitialOrderOutputModel> orders)
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
                    _end = _orders.Count-1;
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
                    _start = _end-2;
                }
            }
            else if (message.Data == "bck")
            {
                context.State = new ClientOrdersMenuState(_messageId);
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
                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Новых заказов нет.", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else if (_orders.Count <= 3)
            {
                string orderInfo = "История заказов:\n";

                for (int i = _start; i <= _end; i++)
                {
                    orderInfo += $"ID заказа: {_orders[i].Id}\nОписание: {_orders[i].OrderDescription}\nСтатус: {_orders[i].StatusDescription}";
                }

               InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
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
                string orderInfo = "История заказов:\n";

                for (int i = _start; i <= _end; i++)
                {
                    orderInfo += $"ID заказа: {_orders[i].Id}\nОписание: {_orders[i].OrderDescription}\nСтатус: {_orders[i].StatusDescription}";
                }

                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
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
    }
}
