using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.ContractorStates;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingContractorServiceType
{
    public class CompleteUpdateContractorServiceTypeState: AbstractState
    {
        public ContractorServiceTypeInputModel ContractorServiceTypeInputModel { get; set; }

        private int _messageId;

        public CompleteUpdateContractorServiceTypeState(ContractorServiceTypeInputModel contractorServiceTypeInputModel)
        {
            ContractorServiceTypeInputModel = contractorServiceTypeInputModel;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
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
            ServiceTypeService serviceTypeService = new ServiceTypeService();

            var service = serviceTypeService.UpdateContractorServiceCostById(ContractorServiceTypeInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Обновление услуги завершено.");

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
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
