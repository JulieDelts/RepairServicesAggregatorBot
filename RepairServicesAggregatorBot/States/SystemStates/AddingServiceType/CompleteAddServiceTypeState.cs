using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using RepairServicesAggregatorBot.Bot.States.AdminStates;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType
{
    public class CompleteAddServiceTypeState : AbstractState
    {
        public ServiceTypeInputModel ServiceTypeInputModel { get; set; }

        public CompleteAddServiceTypeState(ServiceTypeInputModel serviceTypeInputModel)
        {
            ServiceTypeInputModel = serviceTypeInputModel;
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
            else 
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ServiceTypeService serviceTypeService = new ServiceTypeService();

            serviceTypeService.AddServiceType(ServiceTypeInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Добавление услуги завершено.");

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Назад к меню услуг:", replyMarkup: keyboard);
        }
    }
}
