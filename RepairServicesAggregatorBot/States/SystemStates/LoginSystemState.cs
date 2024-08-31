using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates
{
    public class LoginSystemState : AbstractState
    {
        UserInputModel UserInputModel { get; set; } = new();

        bool IsLoginError = false;

        public override void HandleMessage(Context context, Update update)
        {

            var msg = update.Message;

            if (msg.Text == "qwe")
            {
                UserInputModel.RoleId = 3;
                context.State = new RegisterNameSystemState(UserInputModel);
            }
            else if (msg.Text == "собакачерепаха")
            {
                UserInputModel.RoleId = 2;
                context.State = new RegisterNameSystemState(UserInputModel);
            }
            else if (msg.Text == "NO")
            {
                UserInputModel.RoleId = 1;
                context.State = new RegisterNameSystemState(UserInputModel);
            }
            else
            {
                IsLoginError = true;
            }

        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (IsLoginError)
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
