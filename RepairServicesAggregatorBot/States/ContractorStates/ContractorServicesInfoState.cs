using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesProviderBot.BLL;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ContractorServicesInfoState: AbstractState
    {
        ContractorWithServiceTypesOutputModel ContractorWithServiceTypesOutputModel { get; set; }

        public ContractorServicesInfoState(ContractorWithServiceTypesOutputModel contractorWithServiceTypesOutputModel) 
        {
            ContractorWithServiceTypesOutputModel = contractorWithServiceTypesOutputModel;
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
                context.State = new AdminContractorsMenuState();
            }
            else 
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            string contractorInfo = $"{ContractorWithServiceTypesOutputModel.Name}\nРейтинг: {ContractorWithServiceTypesOutputModel.Rating}\nТелефон: {ContractorWithServiceTypesOutputModel.Phone}\nЭлектронная почта: {ContractorWithServiceTypesOutputModel.Email}\nДоступные услуги:\n";

            for (int i = 0; i < ContractorWithServiceTypesOutputModel.ServiceTypes.Count; i++)
            {
                contractorInfo += $"{i+1}. {ContractorWithServiceTypesOutputModel.ServiceTypes[i].ServiceTypeDescription} {ContractorWithServiceTypesOutputModel.ServiceTypes[i].Cost}\n";
            }

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), contractorInfo, replyMarkup: keyboard);
        }
    }
}
