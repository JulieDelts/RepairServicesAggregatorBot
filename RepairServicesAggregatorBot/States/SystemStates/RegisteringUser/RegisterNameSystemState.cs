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
    public class RegisterNameSystemState : AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private bool _isNameError;

        public RegisterNameSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
            _isNameError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsNameValid(message.Text))
            {
                UserInputModel.Name = message.Text;
                context.State = new RegisterPhoneSystemState(UserInputModel);
            }
            else
            {
                _isNameError = true;
            }

        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isNameError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Имя введено некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите имя кириллицей:");
            }
        }

        private bool IsNameValid(string name)
        {
            return Regex.IsMatch(name, @"^[а-яА-ЯёЁ]+ ?[а-яА-ЯёЁa-zA-Z]+ ?[а-яА-ЯёЁa-zA-Z]+$");
        }
    }
}
