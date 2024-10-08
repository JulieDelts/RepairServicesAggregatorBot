﻿using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingServiceType
{
    public class CompleteUpdateServiceTypeSystemState : AbstractState
    {
        ExtendedServiceTypeInputModel ExtendedServiceTypeInputModel { get; set; }

        private int _messageId;

        public CompleteUpdateServiceTypeSystemState(ExtendedServiceTypeInputModel extendedServiceTypeInputModel)
        {
            ExtendedServiceTypeInputModel = extendedServiceTypeInputModel;

            _messageId = 0;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new AdminServiceTypeMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ServiceTypeService serviceTypeService = new();

            var service = serviceTypeService.UpdateServiceType(ExtendedServiceTypeInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Обновление услуги завершено.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Вернуться в меню:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
