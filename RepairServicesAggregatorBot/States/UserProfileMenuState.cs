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
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo;

namespace RepairServicesAggregatorBot.Bot.States
{
    public class UserProfileMenuState : AbstractState
    {
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                if (context.RoleId == 1)
                {
                    context.State = new ClientMenuState();
                }
                else if (context.RoleId == 2)
                {
                    //
                }
                else if (context.RoleId == 3)
                {
                    context.State = new AdminMenuState();
                }
            }
            else if (message.Data == "updprf")
            {
                UserInputModel userInputModel = new UserInputModel();

                context.State = new GetNameSystemState(userInputModel);
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
