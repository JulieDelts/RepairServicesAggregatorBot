using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ConfirmOrderContractorState : AbstractState
    {
        private UnassignedOrderOutputModel _order;

        private OrderService _orderService;

        public ConfirmOrderContractorState(UnassignedOrderOutputModel order)
        {
            _order = order;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "cnfrm")
            {
                
            }
            else if (message.Data == "cncl")
            {

            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var orderInfo = $"Вам заказ!!\nОписание: {_order.OrderDescription}\nАдрес: {_order.Address}";

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
    }
}
