using RepairServicesAggregatorBot.Bot.States.AdminStates;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.Core.OutputModels;
using System.Text;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ContractorServicesInfoState: AbstractState
    {
        ContractorWithServiceTypesOutputModel ContractorWithServiceTypesOutputModel { get; set; }

        private int _messageId;

        public ContractorServicesInfoState(ContractorWithServiceTypesOutputModel contractorWithServiceTypesOutputModel) 
        {
            ContractorWithServiceTypesOutputModel = contractorWithServiceTypesOutputModel;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { 
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new AdminContractorsMenuState(_messageId);
            }
            else 
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            StringBuilder contractorInfo = new($"{ContractorWithServiceTypesOutputModel.Name}\nРейтинг: {ContractorWithServiceTypesOutputModel.Rating}\nТелефон: {ContractorWithServiceTypesOutputModel.Phone}\nЭлектронная почта: {ContractorWithServiceTypesOutputModel.Email}\nДоступные услуги:\n");

            for (int i = 0; i < ContractorWithServiceTypesOutputModel.ServiceTypes.Count; i++)
            {
                contractorInfo.Append($"{i+1}. {ContractorWithServiceTypesOutputModel.ServiceTypes[i].ServiceTypeDescription} {ContractorWithServiceTypesOutputModel.ServiceTypes[i].Cost}\n");
            }

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), contractorInfo.ToString(), replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
