using System.Text.RegularExpressions;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingUserProfile;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.GettingUserProfileInfo
{
    public class GetEmailSystemState : AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        private bool _isEmailError;

        public GetEmailSystemState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;

            _isEmailError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsEmailValid(message.Text))
            {
                UserInputModel.Email = message.Text;

                ChangeState(context);
            }
            else if (message.Text == "no")
            {
                ChangeState(context);
            }
            else
            {
                _isEmailError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isEmailError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверный ввод. Введите электронную почту или 'no'.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите электронную почту или 'no', чтобы перейти к следующему этапу регистрации:");
            }
        }

        private void ChangeState(Context context)
        {
            if (context.Id == 0)
            {
                context.State = new CompleteRegistrationSystemState(UserInputModel);
            }
            else
            {
                context.State = new CompleteUpdateUserProfileSystemState(UserInputModel);
            }
        }

        private bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
