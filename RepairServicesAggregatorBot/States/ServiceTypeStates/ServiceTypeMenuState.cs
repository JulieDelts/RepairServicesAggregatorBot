using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingServiceType;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ServiceTypeStates
{
    public class ServiceTypeMenuState : AbstractState
    {
        public ExtendedServiceTypeInputModel ExtendedServiceTypeInputModel { get; set; }

        public ServiceTypeMenuState(ExtendedServiceTypeInputModel extendedServiceTypeInputModel)
        {
            ExtendedServiceTypeInputModel = extendedServiceTypeInputModel;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new AdminServiceTypeMenuState();
            }
            else if (message.Data == "updsrvtp")
            {
                context.State = new StartUpdateServiceTypeSystemState(ExtendedServiceTypeInputModel);
            }
            else 
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }
        
        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Изменить описание услуги", "updsrvtp"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Скрыть услугу", "hdsrvtp"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                }
            });

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Меню услуги\nОписание: {ExtendedServiceTypeInputModel.ServiceTypeDescription}", replyMarkup: keyboard);
        }
    }
}
