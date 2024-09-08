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

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            _orderRepository.HideOrderById(_orderId);
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            throw new NotImplementedException();
        }

        public override void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            throw new NotImplementedException();
        }
    }
}
