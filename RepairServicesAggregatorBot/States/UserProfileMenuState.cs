using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States
{
    public class UserProfileMenuState : AbstractState
    {
        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
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
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            UserService userService = new UserService();

            var user = userService.GetUserById(context.Id);

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Обновить профиль", "updprf"),
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Ваш профиль\nИмя: {user.Name}\nТелефон: {user.Phone}\nЭлектронная почта: {user.Email}", replyMarkup: keyboard);
        }
    }
}
