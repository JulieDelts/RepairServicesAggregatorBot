using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.BLL;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.SystemStates;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates
{
    public class InitingBaseOrderState : AbstractState
    {
        public OrderInputModel Order { get; set; }

        public InitingBaseOrderState(OrderInputModel order) 
        {
            Order = order;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var msg = update.Message;

            var client = new UserInputModel();
            client.RoleId = 0;
            client.Name = msg.ForwardFrom.Username;
            client.ChatId = msg.Chat.Id;

            Order.StatusId = 1;
            Order.Client = client;

            var orderService = new OrderService();

            //var response = orderService.AddOrder(Order) ?? new ConfirmedOrderOutputModel();

           // context.State = new CompleteCreatingBaseOrderState(response);
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Подгружаем...");
        }
    }
}
