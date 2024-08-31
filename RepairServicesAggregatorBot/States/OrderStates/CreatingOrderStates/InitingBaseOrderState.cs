using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.BLL;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingOrderStates
{
    public class InitingBaseOrderState : AbstractState
    {
        public OrderInputModel Order { get; set; }

        public InitingBaseOrderState(OrderInputModel order) 
        {
            Order = order;
        }

        public override void HandleMessage(Context context, Update update)
        {
            Order.StatusId = 1;
            Order.ClientId = update.Message.Chat.Id;
        }

        public override void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            throw new NotImplementedException();
        }
    }
}
