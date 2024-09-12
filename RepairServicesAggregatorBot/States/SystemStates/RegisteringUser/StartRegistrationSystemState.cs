using RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser
{
    public class StartRegistrationSystemState : AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private Dictionary<string, int> _roleModes;

        private bool _isLoginError;

        public StartRegistrationSystemState()
        {
            UserInputModel = new();

            _roleModes = new Dictionary<string, int>() { { "qwe", 3 }, { "qwo", 2 }, { "no", 1 } };

            _isLoginError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (_roleModes.ContainsKey(message.Text))
            {
                if (message.Text == "qwe")
                {
                    UserInputModel.RoleId = 3;
                    context.RoleId = 3;
                }
                else if (message.Text == "qwo")
                {
                    UserInputModel.RoleId = 2;
                    context.RoleId = 2;
                }
                else if (message.Text == "no")
                {
                    UserInputModel.RoleId = 1;
                    context.RoleId = 1;
                }

                UserInputModel.ChatId = context.ChatId;

                context.State = new GetNameSystemState(UserInputModel);
            }
            else
            {
                _isLoginError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isLoginError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверный вввод. Введите пароль или 'no'.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите пароль для входа в качестве администратора или сотрудника или введите 'no' для продолжения в качестве клиента:");
            }
        }
    }
}
