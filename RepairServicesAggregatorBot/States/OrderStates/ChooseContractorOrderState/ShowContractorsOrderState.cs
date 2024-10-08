﻿using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot.Types.ReplyMarkups;
using RepairServicesProviderBot.Core.InputModels;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ChooseContractorOrderState
{
    public class ShowContractorsOrderState : AbstractState
    {
        private List<ContractorWithServiceTypeOutputModel> _contractors;

        private int _messageId;

        private UnassignedOrderOutputModel _order;

        private int _counter;

        private OrderService _orderService;

        private Dictionary<long, Context> _users;

        public ShowContractorsOrderState(int messageId, UnassignedOrderOutputModel order)
        {
            _messageId = messageId;

            _orderService = new OrderService();

            _contractors = _orderService.GetGetContractorsReadyToAcceptOrderByOrderId(order.Id);

            _counter = 0;

            _order = order;

            _users = Program.Users;
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
                var order = _orderService.GetOrderSystemInfoById(_order.Id);

                var updatedOrder = new ExtendedOrderInputModel()
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    ContractorId = _contractors[_counter].Id,
                    AdminId = order.AdminId,
                    StatusId = 3,
                    ServiceTypeId = order.ServiceTypeId,
                    Date = order.Date,
                    OrderDescription = order.OrderDescription,
                    Address = order.Address,
                    IsDeleted = order.IsDeleted
                };

                _orderService.UpdateOrder(updatedOrder);

                var contractorId = _contractors[_counter].ChatId;

                _users[contractorId].State = new StartExecutingOrderContractorState(_order);
                _users[contractorId].State.ReactInBot(_users[contractorId], botClient);

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
                InlineKeyboardMarkup keyboard = new(
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

                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Хочу такого домой", "choose")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, contractorDescription, replyMarkup: keyboard);

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

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, contractorDescription, replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
        }
    }
}
