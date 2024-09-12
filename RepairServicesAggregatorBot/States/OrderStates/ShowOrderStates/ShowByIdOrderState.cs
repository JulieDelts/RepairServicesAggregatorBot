using RepairServicesProviderBot.DAL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ShowOrderStates
{
    public class ShowByIdOrderState : AbstractState
    {
        private OrderRepository _orderRepository;

        public ShowByIdOrderState() 
        {
            _orderRepository = new();
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data.StartsWith("show_order"))
            {
                var orderId = Convert.ToInt32(message.Data.Split("show_order")[1]);

                var order = _orderRepository.GetOrderById(orderId);

                var orderMessage = $"заказ:\nID заказа: {order.Id}\nОписание: {order.OrderDescription}\nАдрес: {order.Address}\nДата создания: {order.Date}\nСтатус: {order.StatusDescription}\n";

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), orderMessage);
            }
        }

        public override void ReactInBot(Context context, ITelegramBotClient botClient)
        {}
    }
}
