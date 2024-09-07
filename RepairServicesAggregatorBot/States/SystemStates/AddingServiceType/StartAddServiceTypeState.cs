﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType
{
    public class StartAddServiceTypeState : AbstractState
    {
        public ServiceTypeInputModel ServiceTypeInputModel { get; set; }

        private bool _isDescriptionError;

        public StartAddServiceTypeState()
        {
            ServiceTypeInputModel = new ServiceTypeInputModel();

            _isDescriptionError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (!string.IsNullOrEmpty(message.Text))
            {
                ServiceTypeInputModel.ServiceTypeDescription = message.Text;

                context.State = new CompleteAddServiceTypeState(ServiceTypeInputModel);
            }
            else
            {
                _isDescriptionError = true;
            }

        }

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isDescriptionError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Описание введено некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите описание услуги:");
            }
        }
    }
}
