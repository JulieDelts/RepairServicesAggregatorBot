using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingServiceType
{
    public class GetServiceTypeStatusSystemState: AbstractState
    {
        public ExtendedServiceTypeInputModel ExtendedServiceTypeInputModel { get; set; }

        private bool _isStatusError;

        public GetServiceTypeStatusSystemState(ExtendedServiceTypeInputModel extendedServiceTypeInputModel) 
        { 
            ExtendedServiceTypeInputModel = extendedServiceTypeInputModel;

            _isStatusError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsStatusValid(message.Text.ToLower()))
            {
                bool status = Convert.ToBoolean(message.Text);

                ExtendedServiceTypeInputModel.IsDeleted = status;

                context.State = new CompleteUpdateServiceTypeSystemState(ExtendedServiceTypeInputModel);
            }
            else
            {
                _isStatusError = true;
            }
        }

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isStatusError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Описание введено некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Уточните, деактивирована ли услуга (true, false):");
            }
        }

        private bool IsStatusValid(string status)
        {
            return Regex.IsMatch(status, @"true|false");
        }
    }
}
