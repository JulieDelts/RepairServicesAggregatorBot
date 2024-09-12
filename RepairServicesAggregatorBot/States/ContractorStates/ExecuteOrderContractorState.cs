using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ExecuteOrderContractorState: AbstractState
    {
        public ExtendedOrderInputModel ExtendedOrderInputModel { get; set; }

        private int _messageId;

        public ExecuteOrderContractorState(int messageId, ExtendedOrderInputModel extendedOrderInputModel)
        {
            ExtendedOrderInputModel = extendedOrderInputModel;

            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new ContractorOrdersMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

            OrderService orderService = new();

            var orderId = orderService.UpdateOrder(ExtendedOrderInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Заказ ID {orderId} перешел на стадию выполнения.");

            UserService userService = new(); 

            var client = userService.GetUserById(ExtendedOrderInputModel.ClientId);

            long clientChatId = client.ChatId;

            await botClient.SendTextMessageAsync(new ChatId(clientChatId), $"Заказ ID {orderId} перешел на стадию выполнения.");

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
