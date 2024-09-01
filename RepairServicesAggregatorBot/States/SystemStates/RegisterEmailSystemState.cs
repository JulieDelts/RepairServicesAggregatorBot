﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.BLL;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates
{
    public class RegisterEmailSystemState: AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private bool _isEmailError;

        public RegisterEmailSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
            _isEmailError = false;
        }

        public override void HandleMessage(Context context, Update update)
        {
            var message = update.Message;

            if (!string.IsNullOrWhiteSpace(message.Text))
            {
                UserInputModel.Email = message.Text;
                ClientService clientService = new ClientService();
                int qwe = clientService.AddClient(UserInputModel);
                Console.WriteLine(qwe);
                context.State = new CompleteRegistrationSystemState(UserInputModel);
            }
            else if (message.Text == "no")
            {
                context.State = new CompleteRegistrationSystemState(UserInputModel);
            }
            else
            {
                _isEmailError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isEmailError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверный ввод. Введите электронную почту или 'no'.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите электронную почту или 'no', чтобы перейти к следующему этапу регистрации:");
            }
        }
    }
}
