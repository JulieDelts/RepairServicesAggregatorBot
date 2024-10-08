﻿using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Text.RegularExpressions;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo
{
    public class GetNameSystemState : AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private bool _isNameError;

        private int _messageId;

        public GetNameSystemState(UserInputModel userInputModel, int messageId = 0)
        {
            UserInputModel = userInputModel;

            _isNameError = false;

            _messageId = messageId;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsNameValid(message.Text))
            {
                UserInputModel.Name = message.Text;

                context.State = new GetPhoneSystemState(UserInputModel);
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
                if (_messageId != 0)
                {
                    await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);
                }

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите имя кириллицей:");
            }
        }

        private bool IsNameValid(string name)
        {
            return Regex.IsMatch(name, @"^[а-яА-ЯёЁ]+ ?[а-яА-ЯёЁa-zA-Z]+ ?[а-яА-ЯёЁa-zA-Z]+");
        }
    }
}
