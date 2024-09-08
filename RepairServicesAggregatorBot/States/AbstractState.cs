using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States
{
    public abstract class AbstractState
    {
        public abstract void HandleMessage(Context context, Update update, ITelegramBotClient botClient);

        public abstract void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient);

        //public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        //{
        //    await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        //}
        //Заменить и перегружать только в случае прихода колбэка
        public abstract void ReactInBot(Context context, ITelegramBotClient botClient);
    }
}
