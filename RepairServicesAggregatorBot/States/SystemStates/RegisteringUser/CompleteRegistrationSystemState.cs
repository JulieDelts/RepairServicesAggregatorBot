using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser
{
    public class CompleteRegistrationSystemState : AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private int _messageId;

        public CompleteRegistrationSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "mn")
            {
                if (context.RoleId == 1)
                {
                    context.State = new ClientMenuState(_messageId);
                }
                else if (context.RoleId == 2)
                {
                    context.State = new ContractorMenuState(_messageId);
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
            UserService adminService = new();

            int qwe = adminService.AddUser(UserInputModel);

            context.Id = qwe;

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Регистрация завершена.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Меню", "mn")
                }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Перейти к меню:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
