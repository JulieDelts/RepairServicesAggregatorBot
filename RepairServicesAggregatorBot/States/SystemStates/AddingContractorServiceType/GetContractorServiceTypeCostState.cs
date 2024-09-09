using System.Text.RegularExpressions;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingContractorServiceType
{
    public class GetContractorServiceTypeCostState : AbstractState
    {
        public ContractorServiceTypeInputModel ContractorServiceTypeInputModel { get; set; }

        private bool _isCostError;

        public GetContractorServiceTypeCostState(ContractorServiceTypeInputModel contractorServiceTypeInputModel)
        {
            ContractorServiceTypeInputModel = contractorServiceTypeInputModel;

            _isCostError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsCostValid(message.Text))
            {
                int cost = Convert.ToInt32(message.Text);

                ContractorServiceTypeInputModel.Cost = cost;

                context.State = new CompleteAddContractorServiceTypeState(ContractorServiceTypeInputModel);
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
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите стоимость услуги:");
            }
        }

        private bool IsCostValid(string id)
        {
            return Regex.IsMatch(id, @"[0-9]+");
        }
    }
}
