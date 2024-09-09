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
        private int _messageId;

        public AdminServiceTypeMenuState(int messageId)
        {
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nwsrvtp")
            {
                context.State = new StartAddServiceTypeState(_messageId);
            }
            else if (message.Data == "srvtp")
            {
                context.State = new AvailableServiceTypesState(_messageId);
            }
            else if (message.Data == "bck")
            {
                context.State = new AdminMenuState(_messageId);
            }
            else if (message.Data == "cntrctr")
            {
                context.State = new GetServiceTypeState(_messageId);
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
                    InlineKeyboardButton.WithCallbackData("Доступные услуги", "srvtp"),
                    InlineKeyboardButton.WithCallbackData("Добавление услуги", "nwsrvtp"),
                        
                },
                new[]
                {   
                    InlineKeyboardButton.WithCallbackData("Меню услуги", "cntrctr"),
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                }
            });

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню услуг", replyMarkup: keyboard);

            _messageId = message.MessageId; 
        }
    }
}
