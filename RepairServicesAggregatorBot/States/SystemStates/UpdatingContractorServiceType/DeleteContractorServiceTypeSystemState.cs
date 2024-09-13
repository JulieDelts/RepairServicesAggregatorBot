using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingContractorServiceType
{
    public class DeleteContractorServiceTypeSystemState: AbstractState
    {
        public ContractorServiceTypeInputModel ContractorServiceTypeInputModel { get; set; }

        private int _messageId;

        public DeleteContractorServiceTypeSystemState(int messageId, ContractorServiceTypeInputModel contractorServiceTypeInputModel) 
        { 
            _messageId = messageId;

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
            await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

            ServiceTypeService serviceTypeService = new();

            serviceTypeService.DeleteContractorServiceType(ContractorServiceTypeInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Удаление услуги завершено.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Вернуться в меню:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
