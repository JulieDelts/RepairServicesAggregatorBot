using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingUserProfile
{
    public class CompleteUpdateUserProfileSystemState : AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private int _messageId;

        public CompleteUpdateUserProfileSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
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
                if (context.RoleId == 1)
                {
                    context.State = new ClientMenuState();
                }
                else if (context.RoleId == 2)
                {
                }
                else if (context.RoleId == 3)
                {
                    context.State = new AdminMenuState(_messageId);
                }
            }
            else 
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            UserService userService = new UserService();

            var client = userService.GetUserById(context.Id);

            var updatedClient = new ExtendedUserInputModel()
            {
                Id = context.Id,
                Name = UserInputModel.Name,
                Email = UserInputModel.Email,
                Phone = UserInputModel.Phone,
                ChatId = client.ChatId,
                RoleId = client.RoleId,
                IsDeleted = client.IsDeleted,
                Image = client.Image
            };

            userService.UpdateUserById(updatedClient);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Обновление профиля завершено.");

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
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
