using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingServiceType
{
    public class StartUpdateServiceTypeSystemState : AbstractState
    {
        public ExtendedServiceTypeInputModel ExtendedServiceTypeInputModel { get; set; }

        private bool _isDescriptionError;

        public StartUpdateServiceTypeSystemState(ExtendedServiceTypeInputModel serviceTypeOutputModel)
        {
            ExtendedServiceTypeInputModel = serviceTypeOutputModel;

            _isDescriptionError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (!string.IsNullOrEmpty(message.Text))
            {
               ExtendedServiceTypeInputModel.ServiceTypeDescription = message.Text;
                
               context.State = new GetServiceTypeStatusSystemState(ExtendedServiceTypeInputModel);
            }
            else
            {
                _isDescriptionError = true;
            }
        }

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isDescriptionError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Описание введено некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите описание услуги:");
            }
        }
    }
}
