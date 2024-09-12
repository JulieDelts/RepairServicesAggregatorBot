using System.Text;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ServiceTypeStates
{
    public class AvailableServiceTypesState : AbstractState
    {
        private int _messageId;

        public AvailableServiceTypesState(int messageId)
        {
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                if (context.RoleId == 1)
                {
                    context.State = new ClientMenuState(_messageId);
                }
                else if (context.RoleId == 3)
                {
                    context.State = new AdminServiceTypeMenuState(_messageId);
                }
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ServiceTypeService serviceTypeService = new();

            var services = serviceTypeService.GetAvailableServices();

            StringBuilder servicesDescription = new("Услуги:\n");

            for (int i = 0; i < services.Count; i++)
            {
                if (context.RoleId == 1)
                {
                    if (services[i].IsDeleted == true)
                    {
                        continue;
                    }

                    servicesDescription.Append($"{i + 1}. {services[i].ServiceTypeDescription}\n");
                }
                else
                {
                    servicesDescription.Append($"{i + 1}. {services[i].ServiceTypeDescription}\nID: {services[i].Id}\nСкрыт: {services[i].IsDeleted}\n");
                }
            }

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, servicesDescription.ToString(), replyMarkup: keyboard);

            int messageId = message.MessageId;
        }
    }
}
