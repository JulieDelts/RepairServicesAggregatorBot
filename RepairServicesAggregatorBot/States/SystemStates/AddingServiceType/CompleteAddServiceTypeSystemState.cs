using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using RepairServicesAggregatorBot.Bot.States.AdminStates;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType
{
    public class CompleteAddServiceTypeSystemState : AbstractState
    {
        public ServiceTypeInputModel ServiceTypeInputModel { get; set; }

        private int _messageId;

        public CompleteAddServiceTypeSystemState(ServiceTypeInputModel serviceTypeInputModel)
        {
            ServiceTypeInputModel = serviceTypeInputModel;
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

            serviceTypeService.AddServiceType(ServiceTypeInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Добавление услуги завершено.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Назад к меню услуг:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
