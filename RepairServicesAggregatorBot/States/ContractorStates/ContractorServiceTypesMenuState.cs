using RepairServicesAggregatorBot.Bot.States.SystemStates.AddingContractorServiceType;
using RepairServicesProviderBot.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ContractorServiceTypesMenuState : AbstractState
    {
        private int _messageId;

        public ContractorServiceTypesMenuState(int messageId)
        {
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "addsrvtp")
            {
                context.State = new StartAddContractorServiceTypeSystemState(_messageId);
            }
            else if (message.Data == "bck")
            {
                context.State = new ContractorMenuState(_messageId);
            }
            else if (message.Data == "сsrvtps")
            {
                ServiceTypeService serviceTypeService = new();

                var contractorServiceTypes = serviceTypeService.GetContractorServiceTypesById(context.Id);

                context.State = new ContractorServiceTypeState(_messageId, contractorServiceTypes);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Мои услуги", "сsrvtps"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Добавить услугу", "addsrvtp"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                },
            });

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню услуг сотрудника", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
