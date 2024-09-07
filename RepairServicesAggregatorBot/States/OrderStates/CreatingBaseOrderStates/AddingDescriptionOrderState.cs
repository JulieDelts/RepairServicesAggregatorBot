﻿using Telegram.Bot;
using Telegram.Bot.Types;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates
{
    public class AddingDescriptionOrderState : AbstractState
    {
        public OrderInputModel Order { get; set; }

        public AddingDescriptionOrderState() 
        {
            Order = new OrderInputModel();
        }
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var msg = update.Message;

            if (msg.Text=="Назад")
            {
                context.State = new StartRegistrationSystemState();
            }
            else
            {
                Order.OrderDescription = msg.Text;
                context.State = new AddingAdressOrderState(Order);
            }
        }

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введи описание заказа");
        }
    }
}
