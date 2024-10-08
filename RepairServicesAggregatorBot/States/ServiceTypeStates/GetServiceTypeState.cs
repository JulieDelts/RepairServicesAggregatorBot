﻿using System.Text.RegularExpressions;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.ServiceTypeStates
{
    public class GetServiceTypeState : AbstractState
    {
        private bool _isIdError;

        private int _messageId;

        public GetServiceTypeState(int messageId)
        {
            _messageId = messageId;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsIdValid(message.Text))
            {
                try
                {
                    int id = Convert.ToInt32(message.Text);

                    ServiceTypeService serviceTypeService = new();

                    var serviceType = serviceTypeService.GetServiceTypeById(id);

                    ExtendedServiceTypeInputModel inputModel = new();

                    inputModel.Id = id;

                    inputModel.ServiceTypeDescription = serviceType.ServiceTypeDescription;

                    context.State = new ServiceTypeMenuState(inputModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    _isIdError = true;
                }
            }
            else
            {
                _isIdError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isIdError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "ID введен некорректно.");
            }
            else
            {
                await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите ID услуги:");
            }
        }

        private bool IsIdValid(string id)
        {
            return Regex.IsMatch(id, @"[0-9]+");
        }
    }
}
