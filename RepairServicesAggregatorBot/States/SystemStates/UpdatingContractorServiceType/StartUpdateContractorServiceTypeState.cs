using System.Text.RegularExpressions;
using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingContractorServiceType;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingContractorServiceType
{
    public class StartUpdateContractorServiceTypeState : AbstractState
    {
        public ContractorServiceTypeInputModel ContractorServiceTypeInputModel { get; set; }

        private int _messageId;

        private bool _isCostError;

        public StartUpdateContractorServiceTypeState(int messageId, ContractorServiceTypeInputModel contractorServiceTypeInputModel)
        {
            _messageId = messageId;

            ContractorServiceTypeInputModel = contractorServiceTypeInputModel;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsCostValid(message.Text))
            {
                int cost = Convert.ToInt32(message.Text);

                ContractorServiceTypeInputModel.Cost = cost;

                context.State = new CompleteUpdateContractorServiceTypeState(ContractorServiceTypeInputModel);
            }
            else
            {
                _isCostError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isCostError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Стоимость введена некорректно.");
            }
            else
            {
                await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите стоимость услуги:");
            }
        }

        private bool IsCostValid(string id)
        {
            return Regex.IsMatch(id, @"[0-9]+");
        }

    }
}
