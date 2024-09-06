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
        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                if (context.RoleId == 1)
                {
                    context.State = new ClientMenuState();
                }
                else if (context.RoleId == 2)
                {
                    //
                }
                else if (context.RoleId == 3)
                {
                    context.State = new AdminMenuState();
                }
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ServiceTypeService serviceTypeService = new ServiceTypeService();

            var services = serviceTypeService.GetAvailableServices();

            string servicesDescription = "Доступные услуги:\n";

            for (int i = 0; i < services.Count; i++)
            {
                servicesDescription += $"{i + 1}. {services[i].ServiceTypeDescription}\n";
            }

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
               new[]
               {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
               }
           );

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), servicesDescription, replyMarkup: keyboard);
        }
    }
}
