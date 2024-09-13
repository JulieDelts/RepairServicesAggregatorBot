using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingReview;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ManageExistOrderStates
{
    public class CompleteOrderState : AbstractState
    {
        private int _orderId;

        private OrderService _orderService;

        private UserService _userService;

        private int _messageId;

        public CompleteOrderState(int orderId, int messageId)
        {
            _orderId = orderId;

            _orderService = new();

            _userService = new();

            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new ClientOrdersMenuState(_messageId);
            }
            else if (message.Data == "rev")
            {
                context.State = new StartAddReviewSystemState(_messageId, _orderId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var order = _orderService.GetOrderSystemInfoById(_orderId);

            var updatedOrder = new ExtendedOrderInputModel()
            {
                Id = order.Id,
                ClientId = order.ClientId,
                ContractorId = order.ContractorId,
                AdminId = order.AdminId,
                StatusId = 5,
                ServiceTypeId = order.ServiceTypeId,
                Date = order.Date,
                OrderDescription = order.OrderDescription,
                Address = order.Address,
                IsDeleted = order.IsDeleted

            };

            _orderService.UpdateOrder(updatedOrder);

            var admin = _userService.GetUserById((int)order.AdminId);

            long adminChatId = admin.ChatId;

            await botClient.SendTextMessageAsync(adminChatId, $"Заказ {updatedOrder.Id} выполнен.");

            var contractor = _userService.GetUserById((int)order.ContractorId);

            long contractorChatId = contractor.ChatId;

            await botClient.SendTextMessageAsync(contractorChatId, $"Заказ {updatedOrder.Id} выполнен.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Оставить отзыв", "rev"),
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"Ваш заказ {updatedOrder.Id} выполнен. Вы можете оставить отзыв или вернуться в меню.", replyMarkup:keyboard);
        }
    }
}
