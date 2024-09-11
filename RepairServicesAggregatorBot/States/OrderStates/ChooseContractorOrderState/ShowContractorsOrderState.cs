using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Diagnostics.Metrics;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ChooseContractorOrderState
{
    public class ShowContractorsOrderState : AbstractState
    {
        private List<ContractorWithServiceTypeOutputModel> _contractors;

        private int _messageId;

        private UnassignedOrderOutputModel _order;

        private int _counter;

        private ContractorService _contractorService;

        private Dictionary<long, Context> _users;

        public ShowContractorsOrderState(int messageId, UnassignedOrderOutputModel order, int serviceId)
        {
            _messageId = messageId;
            _contractors = _contractorService.GetContractorsByServiceTypeId(serviceId);
            _counter = 0;
            _order = order;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nxt")
            {
                if (_counter != _contractors.Count - 1)
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
                    _counter = _contractors.Count - 1;
                }

            }
            else if (message.Data == "choose")
            {
                var contractorId = _contractors[_counter].ChatId;

                var orderInfo = $"Вам заказ!!\nОписание: {_order.OrderDescription}\nАдрес: {_order.Address}";

                InlineKeyboardMarkup contractorKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Взять в работу", "cnfrm"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Отменить", "cncl")
                    }
                });

                //_users[contractorId].State = new ConfirmOrderContractorState();

                await botClient.SendTextMessageAsync(contractorId, orderInfo, replyMarkup: contractorKeyboard);

                context.State = new ClientOrdersMenuState(_messageId);
            }
            else if (message.Data == "bck")
            {
                context.State = new ClientOrdersMenuState(_messageId);
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_contractors.Count == 0)
            {
                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Сотрудников нет", replyMarkup: keyboard);
            }
            else if (_contractors.Count == 1)
            {

                var contractor = _contractors[0];

                string contractorDescription = $"Сотрудник: {contractor.Name}\nЦена {contractor.ServiceType.Cost}";

                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Хочу такого домой", $"choose")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"{contractorDescription}", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                var contractor = _contractors[_counter];

                string contractorDescription = $"Сотрудник: {contractor.Name}\nЦена {contractor.ServiceType.Cost}";

                InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Хочу такого домой", $"choose{contractor.Id}")

                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("⬅️", "prv"),
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    InlineKeyboardButton.WithCallbackData("➡️", "nxt")
                }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"{contractorDescription}", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
        }
    }
}
