using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using System.Text.RegularExpressions;

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

            if (IsDescriptionValid(message.Text))
            {
                ServiceTypeInputModel.ServiceTypeDescription = message.Text;

                context.State = new CompleteAddServiceTypeState(ServiceTypeInputModel);
            }
            else
            {
                _isDescriptionError = true;
            }
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

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

        private bool IsDescriptionValid(string description)
        {
            return Regex.IsMatch(description, @"^[а-яА-ЯёЁ\s]+");
        }
    }
}
