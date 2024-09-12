using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ConfirmOrderContractorState : AbstractState
    {
        private ExtendedOrderOutputModel _order;

        private OrderService _orderService;

        private ClientService _clientService;

        public ConfirmOrderContractorState(UnassignedOrderOutputModel order)
        {
            _orderService = new OrderService();

            _order = _orderService.GetOrderSystemInfoById(order.Id);
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "cnfrm")
            {
                    int contractorId = context.Id;

                    var updatedOrder = new ExtendedOrderInputModel()
                    {
                        Id = _order.Id,
                        ClientId = _order.ClientId,
                        ContractorId = contractorId,
                        AdminId = context.Id,
                        StatusId = 1,
                        ServiceTypeId = _order.ServiceTypeId,
                        Date = _order.Date,
                        OrderDescription = _order.OrderDescription,
                        Address = _order.Address,
                        IsDeleted = _order.IsDeleted
                    };
                
            }
            else if (message.Data == "cncl")
            {
                context.State = new ContractorMenuState();
            }
        }


        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var client = _clientService.GetClientById(_order.ClientId);

            var orderInfo = $"Вам заказ!!\nОписание: {_order.OrderDescription}\nАдрес: {_order.Address}\n";

            InlineKeyboardMarkup contractorKeyboard = new(
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

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), orderInfo, replyMarkup: contractorKeyboard);
        }

        private bool IsIdValid(string id)
        {
            return Regex.IsMatch(id, @"[0-9]+");
        }
    }
}
