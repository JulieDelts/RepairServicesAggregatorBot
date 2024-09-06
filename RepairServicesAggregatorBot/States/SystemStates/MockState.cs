using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates
{
    public class MockState: AbstractState
    {
        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var msg = update.Message;

            if (update == null || msg == null)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Что-то пошло не так. Введите '/start'.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Что-то пошло не так. Введите '/start'.");
        }
    }
}
