using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.BLL;
using RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingUserProfile;

namespace RepairServicesAggregatorBot.Bot.States.ClientStates
{
    public class ClientProfileMenuState: AbstractState
    {
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
           var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new ClientMenuState();
            }
            else if (message.Data == "updprf")
            {
                context.State = new StartUpdateUserProfileSystemState();
            }

        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ClientService clientService = new ClientService();

            var client = clientService.GetClientById(context.Id);

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
               new[]
               {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Обновить профиль", "updprf"),
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
               }
           );

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Ваш профиль\n{client.Image ?? ""}\nИмя: {client.Name}\nТелефон: {client.Phone}\nЭлектронная почта: {client.Email}", replyMarkup: keyboard);
        }

    }
}
