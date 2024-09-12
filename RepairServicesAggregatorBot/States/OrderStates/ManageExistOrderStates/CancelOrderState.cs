using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using Telegram.Bot.Types.ReplyMarkups;
using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingReview;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ManageExistOrderStates
{
    public class CancelOrderState : AbstractState
    {
        private int _orderId;

        private OrderService _orderService;

        private UserService _userService;

        private int _messageId;

        public CancelOrderState(int orderId, int messageId)
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
                context.State = new StartAddReviewState(_messageId, _orderId);
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
                StatusId = 6,
                ServiceTypeId = order.ServiceTypeId,
                Date = order.Date,
                OrderDescription = order.OrderDescription,
                Address = order.Address,
                IsDeleted = order.IsDeleted
            };

            _orderService.UpdateOrder(updatedOrder);

            if (context.RoleId == 1 && order.StatusId >= 1 && order.StatusId <= 5)
            {   
                var admin = _userService.GetUserById((int)order.AdminId);

                long adminChatId = admin.ChatId;

                await botClient.SendTextMessageAsync(adminChatId, $"Заказ {updatedOrder.Id} отменен.");
            }

            if (order.StatusId >= 3 && order.StatusId <= 5)
            {
                var contractor = _userService.GetUserById((int)order.ContractorId);

                long contractorChatId = contractor.ChatId;

                await botClient.SendTextMessageAsync(contractorChatId, $"Заказ {updatedOrder.Id} отменен.");
            }

            if (context.RoleId == 3 && order.StatusId == 0)
            {
                var client = _userService.GetUserById(order.ClientId);

                long clientChatId = client.ChatId;

                await botClient.SendTextMessageAsync(clientChatId, $"Заказ {updatedOrder.Id} отменен.");
            }

            if (context.RoleId == 1 && order.StatusId >= 3)
            {
                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Оставить отзыв", "rev"),
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
                });

                await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"Ваш заказ {updatedOrder.Id} отменен. Вы можете оставить отзыв или вернуться в меню.", replyMarkup: keyboard);
            }
            else if (context.RoleId == 1)
            {
                await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"Заказ {updatedOrder.Id} отменен.");

                context.State = new ClientMenuState();

                context.State.ReactInBot(context, botClient);
            }
            else if (context.RoleId == 3)
            {
                await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, $"Заказ {updatedOrder.Id} отменен.");

                context.State = new AdminMenuState();

                context.State.ReactInBot(context, botClient);
            }
        }
    }
}
