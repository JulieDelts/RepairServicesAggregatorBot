using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.OutputModels;
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

        private int _messageId;

        private bool _isActive;

        public ConfirmOrderContractorState(UnassignedOrderOutputModel order)
        {
            _orderService = new OrderService();

            _clientService = new ClientService();

            _order = _orderService.GetOrderSystemInfoById(order.Id);

            _messageId = 0;

            _isActive = false;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "cnfrm")
            {
                int contractorId = context.Id;

                _orderService.AddContractorReadyToAcceptOrder(contractorId, _order.Id);

                UserService userService = new();

                var client = userService.GetUserById(_order.ClientId);

                long clientChatId = client.ChatId;

                await botClient.SendTextMessageAsync(new ChatId(clientChatId), $"Заказ ID {_order.Id} готов выполнить еще один сотрудник. Сотрудника можно выбрать в меню выбора сотрудников у заказа.");

                context.State = new ContractorMenuState(_messageId);

                context.State.ReactInBot(context, botClient);
            }
            else if (message.Data == "cncl")
            {
                context.State = new ContractorMenuState(_messageId);

                context.State.ReactInBot(context, botClient);
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isActive) { }
            else
            {
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
                    InlineKeyboardButton.WithCallbackData("Отклонить", "cncl")
                }
                });

                var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), orderInfo, replyMarkup: contractorKeyboard);

                _messageId = message.MessageId;

                _isActive = true;
            }
        }
    }
}
