﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates
{
    public class RegisterNameSystemState: AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private bool _isNameError;

        public RegisterNameSystemState(UserInputModel userInputModel) 
        {
            UserInputModel = userInputModel;
            _isNameError = false;
        }

        public override void HandleMessage(Context context, Update update)
        {
            var message = update.Message;

            if (!string.IsNullOrWhiteSpace(message.Text))
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
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите имя:");
            }
        }
    }
}
