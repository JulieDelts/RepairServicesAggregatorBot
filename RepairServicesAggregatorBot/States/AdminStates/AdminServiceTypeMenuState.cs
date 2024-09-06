using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType;

namespace RepairServicesAggregatorBot.Bot.States.AdminStates
{
    public class AdminServiceTypeMenuState: AbstractState 
    {
        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            if (update.CallbackQuery.Data == "nwsrvtp")
            {
                context.State = new StartAddServiceTypeState();
            }
            else if (update.CallbackQuery.Data == "srvtp")
            {
                context.State = new AvailableServiceTypesState();
            }
            else if (update.CallbackQuery.Data == "bck")
            {
                context.State = new AdminMenuState();
            }
            else if(update.CallbackQuery.Data == "cntrctr")
            {
                context.State = new GetServiceTypeState();
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Доступные услуги", "srvtp"),
                        InlineKeyboardButton.WithCallbackData("Добавление услуги", "nwsrvtp"),
                        
                    },
                    new[]
                    {   
                        InlineKeyboardButton.WithCallbackData("Меню услуги", "cntrctr"),
                        InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                }
            );

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Меню услуг", replyMarkup: keyboard);
        }
    }
}
