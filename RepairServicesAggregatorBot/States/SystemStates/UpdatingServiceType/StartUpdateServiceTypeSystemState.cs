using System.Text.RegularExpressions;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingServiceType
{
    public class StartUpdateServiceTypeSystemState : AbstractState
    {
        public ExtendedServiceTypeInputModel ExtendedServiceTypeInputModel { get; set; }

        private bool _isDescriptionError;

        private int _messageId;

        public StartUpdateServiceTypeSystemState(int messageId, ExtendedServiceTypeInputModel serviceTypeOutputModel)
        {
            ExtendedServiceTypeInputModel = serviceTypeOutputModel;

            _isDescriptionError = false;

            _messageId = messageId;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsDescriptionValid(message.Text))
            {
                ExtendedServiceTypeInputModel.ServiceTypeDescription = message.Text;

                context.State = new GetServiceTypeStatusSystemState(ExtendedServiceTypeInputModel);
            }
            else
            {
                _isDescriptionError = true;

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isDescriptionError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Описание введено некорректно.");
            }
            else
            {
                await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите описание услуги:");
            }
        }

        private bool IsDescriptionValid(string description)
        {
            return Regex.IsMatch(description, @"^[а-яА-ЯёЁ\s]+");
        }
    }
}
