using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States
{
    public abstract class AbstractState
    {
        public abstract void HandleMessage(Context context, Update update, ITelegramBotClient botClient);

        public abstract void ReactInBot(Context context, ITelegramBotClient botClient);
    }
}
