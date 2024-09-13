using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;
using RepairServicesAggregatorBot.Bot.States.AdminStates;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.ConfirmingOrder
{
    public class CompleteConfirmOrderAdminSystemState : AbstractState
    {
        public ExtendedOrderInputModel ExtendedOrderInputModel { get; set; }

        private List<ContractorWithServiceTypeOutputModel> _contractors;

        private int _messageId;

        public CompleteConfirmOrderAdminSystemState(ExtendedOrderInputModel extendedOrderInputModel, List<ContractorWithServiceTypeOutputModel> contractors)
        {
            ExtendedOrderInputModel = extendedOrderInputModel;

            _contractors = contractors;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
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
            OrderService orderService = new();

            var orderId = orderService.UpdateOrder(ExtendedOrderInputModel);

            UnassignedOrderOutputModel unassignedOrderOutputModel = orderService.GetOrderById(orderId) as UnassignedOrderOutputModel ?? new();

            foreach (var contractor in _contractors)
            {
                if (Program.Users.ContainsKey(contractor.ChatId))
                {
                    Program.Users[contractor.ChatId].State = new ConfirmOrderContractorState(unassignedOrderOutputModel);

                    Program.Users[contractor.ChatId].State.ReactInBot(Program.Users[contractor.ChatId], botClient);
                }
            }

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Подтверждение заказа завершено.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Назад к меню заказов:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
