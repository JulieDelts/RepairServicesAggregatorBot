using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates
{
    public class RegisterNameSystemState:AbstractState
    {
        UserInputModel UserInputModel { get; set; }

        bool IsNameError = false;

        public RegisterNameSystemState(UserInputModel userInputModel) 
        {
            UserInputModel = userInputModel;
        }

        public override void HandleMessage(Context context, Update update)
        {
            var msg = update.Message;

            if (!string.IsNullOrWhiteSpace(msg.Text))
            {
                UserInputModel.Name = msg.Text;
                context.State = new LoginSystemState();
            }
            else
            {
                IsNameError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (IsNameError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Имя введено некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите имя.");
            }
        }
    }
}
