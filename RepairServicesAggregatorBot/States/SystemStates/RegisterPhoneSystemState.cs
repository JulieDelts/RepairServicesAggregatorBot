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
    public class RegisterPhoneSystemState: AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private bool _isPhoneError;

        public RegisterPhoneSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
            _isPhoneError = false;
        }

        public override void HandleMessage(Context context, Update update)
        {
            var message = update.Message;

            if (!string.IsNullOrWhiteSpace(message.Text))
            {
                UserInputModel.Phone = message.Text;
                context.State = new RegisterEmailSystemState(UserInputModel);
            }
            else
            {
                _isPhoneError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isPhoneError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Номер введен некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите номер телефона:");
            }
        }
    }
}
