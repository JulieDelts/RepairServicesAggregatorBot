using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingContractorServiceType
{
    public class CompleteAddContractorServiceTypeSystemState: AbstractState
    {
        public ContractorServiceTypeInputModel ContractorServiceTypeInputModel { get; set; }

        private int _messageId;

        public CompleteAddContractorServiceTypeSystemState(ContractorServiceTypeInputModel contractorServiceTypeInputModel)
        {
            ContractorServiceTypeInputModel = contractorServiceTypeInputModel;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new ContractorServiceTypesMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ServiceTypeService serviceTypeService = new();

            serviceTypeService.AddContractorServiceType(ContractorServiceTypeInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Добавление услуги завершено.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Назад к меню услуг:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
