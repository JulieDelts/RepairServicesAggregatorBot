using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ManageExistOrderStates
{
    public class CancelOrderState : AbstractState
    {
        private int _orderId;

        private OrderService _orderService;

        public CancelOrderState(int orderId)
        {
            _orderId = orderId;
            _orderService = new();
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

            if (updatedOrder.StatusId > 1 && updatedOrder.StatusId < 5)
            {
                await botClient.SendTextMessageAsync(updatedOrder.AdminId, $"заказ {updatedOrder.Id} удален!!");
            }

            if (updatedOrder.StatusId > 1 && updatedOrder.StatusId < 5)
            {
                await botClient.SendTextMessageAsync(updatedOrder.ContractorId, $"заказ {updatedOrder.Id} удален!!");
            }

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Ваш заказ удален!!");

            context.State = new ClientMenuState();

            context.State.ReactInBot(context, botClient);
        }
    }
}
