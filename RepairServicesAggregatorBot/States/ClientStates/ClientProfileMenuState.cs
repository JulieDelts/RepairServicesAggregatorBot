using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.ClientStates
{
    public class ClientProfileMenuState: AbstractState
    {
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
           var message = update.Message;

            if (message.Text == "/back")
            {
                context.State = new ClientMenuState();
            }

        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Новое состояние");
        }

    }
}
