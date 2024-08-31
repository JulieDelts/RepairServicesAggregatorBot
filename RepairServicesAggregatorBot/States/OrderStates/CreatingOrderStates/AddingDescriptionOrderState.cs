using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingOrderStates
{
    public class AddingDescriptionOrderState : AbstractState
    {
        public override void HandleMessage(Context context, Update update)
        {
            var msg = update.Message;

            if (msg.Text!="/Назад")
            {

            }
        }

        public override void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            
        }
    }
}
