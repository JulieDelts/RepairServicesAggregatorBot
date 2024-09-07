using RepairServicesProviderBot.Core.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates
{
    public class CompleteCreatingBaseOrderState : AbstractState
    {
        public UnConfirmedOrderOutputModel Responce;

        public CompleteCreatingBaseOrderState(UnConfirmedOrderOutputModel responce)
        {
            Responce = responce;
        }
        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            context.State = new AddingDescriptionOrderState();
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var msg = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"{Responce}");
            await botClient.DeleteMessageAsync(new ChatId(context.ChatId), msg.MessageId - 1);
        }
    }
}
