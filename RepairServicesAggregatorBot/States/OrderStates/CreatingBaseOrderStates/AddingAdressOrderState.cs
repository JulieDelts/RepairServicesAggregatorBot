using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.BLL;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates
{
    public class AddingAdressOrderState : AbstractState
    {
        public OrderInputModel Order { get; set; }

        public AddingAdressOrderState(OrderInputModel order)
        {
            Order = order;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var msg = update.Message;

            if (msg.Text == "Назад")
            {
                context.State = new StartRegistrationSystemState();
            }
            else
            {
                Order.Date = msg.Text;

                var clientService = new ClientService();

                context.State = new InitingBaseOrderState(Order);
            }
        }

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введи свой адрес");
        }
    }
}
