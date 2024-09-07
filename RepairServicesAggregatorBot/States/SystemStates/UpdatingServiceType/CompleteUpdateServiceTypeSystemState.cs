using RepairServicesAggregatorBot.Bot.States.AdminStates;
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

        public CompleteUpdateServiceTypeSystemState(ExtendedServiceTypeInputModel extendedServiceTypeInputModel)
        {
            ExtendedServiceTypeInputModel = extendedServiceTypeInputModel;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new AdminServiceTypeMenuState();
            }
        }

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ServiceTypeService serviceTypeService = new ServiceTypeService();

            var service = serviceTypeService.UpdateServiceTypeById(ExtendedServiceTypeInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Обновление услуги завершено.\nОписание услуги: {service.ServiceTypeDescription}");

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
            });

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Вернуться в меню:", replyMarkup: keyboard);
        }
    }
}
