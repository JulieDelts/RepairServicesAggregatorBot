using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingServiceType;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.BLL;

namespace RepairServicesAggregatorBot.Bot.States.ServiceTypeStates
{
    public class GetServiceTypeState: AbstractState
    {
        private bool _isIdError; 

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (!string.IsNullOrEmpty(message.Text))
            {
                try
                {
                    int id = Convert.ToInt32(message.Text);

                    ServiceTypeService serviceTypeService = new ServiceTypeService();

                    var serviceType = serviceTypeService.GetServiceTypeById(id);

                    ExtendedServiceTypeInputModel inputModel = new ExtendedServiceTypeInputModel();
                    
                    inputModel.Id = id;

                    inputModel.ServiceTypeDescription = serviceType.ServiceTypeDescription;

                    context.State = new ServiceTypeMenuState(inputModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    _isIdError = true;
                }
            }
            else
            {
                _isIdError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isIdError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "ID введен некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите ID услуги:");
            }
        }
    }
}
