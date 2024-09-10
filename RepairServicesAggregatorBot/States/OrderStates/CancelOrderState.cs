using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates
{
    public class CancelOrderState : AbstractState
    {
        private int _orderId;

        private OrderRepository _orderRepository;

        public CancelOrderState(int orderId)
        {
            _orderId = orderId;
            _orderRepository = new OrderRepository();
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            _orderRepository?.HideOrderById(_orderId);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Ваш заказ удален!!");

            context.State = new ClientMenuState();

            context.State.ReactInBot(context, botClient);
        }
    }
}
