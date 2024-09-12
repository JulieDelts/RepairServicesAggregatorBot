using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.OrderStates.ShowOrderStates;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ContractorOrdersMenuState: AbstractState
    {
        private int _messageId;

        public ContractorOrdersMenuState(int messageId) 
        { 
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "co")
            {
                OrderService orderService = new();

                var contractorOrders = orderService.GetAllOrdersByUserId(context.Id);

                List<InitialOrderOutputModel> currentOrders = new();

                foreach (var order in contractorOrders)
                {
                    if ((order.StatusId == 3 || order.StatusId == 4) && order.IsDeleted == false)
                    {
                        currentOrders.Add(order);
                    }
                }

                context.State = new CurrentContractorOrdersMenuState(_messageId, currentOrders);
            }
            else if (message.Data == "ho")
            {
                OrderService orderService = new();

                var contractorOrders = orderService.GetAllOrdersByUserId(context.Id);

                List<InitialOrderOutputModel> inactiveOrders = new();

                foreach (var order in contractorOrders)
                {
                    if ((order.StatusId == 5 || order.StatusId == 6) && order.IsDeleted == false)
                    {
                        inactiveOrders.Add(order);
                    }
                }

                context.State = new OrderHistoryState(_messageId, inactiveOrders);
            }
            else if (message.Data == "bck")
            {
                context.State = new ContractorMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Текущие заказы", "co"),
                        InlineKeyboardButton.WithCallbackData("История заказов", "ho")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
                });

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню заказов сотрудника", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
