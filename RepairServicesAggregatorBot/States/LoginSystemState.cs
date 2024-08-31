using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States
{
    public class LoginSystemState : AbstractState
    {
        bool isLoginError = false;

        public override void HandleMessage(Context context, Update update)
        {
            var msg = update.Message;

            if (msg.Text == "qwe")
            {
                //context.State = new AdminState()
            }
            else if (msg.Text == "собакачерепаха")
            {
                //context.State = new ContractorState()
            }
            else if (msg.Text == "NO")
            {
                //context.State = new UserState()
            }
            else
            {
                isLoginError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (isLoginError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "IDI NAHUI");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "ENTER PASSWORD OR SEND NO");
            }
        }
    }
}
