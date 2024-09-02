using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Text.RegularExpressions;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser
{
    public class RegisterPhoneSystemState : AbstractState
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

            if (IsPhoneValid(message.Text))
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
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите номер телефона в формате +7 ### ###-##-##:");
            }
        }

        private bool IsPhoneValid(string phone)
        {
            return Regex.IsMatch(phone, @"\+7 \d{3} \d{3}-\d{2}-\d{2}");
        }
    }
}
