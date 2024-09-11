using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Diagnostics.Metrics;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.ChooseContractorOrderState
{
    public class ShowContractorsOrderStates : AbstractState
    {
        private List<ContractorWithServiceTypeOutputModel> _contractors;

        private int _messageId;

        private int _counter;

        private OrderService _orderService;

        public ShowContractorsOrderStates(int messageId, int orderId)
        {
            _messageId = messageId;
            _contractors = _orderService.GetGetContractorsReadyToAcceptOrderByOrderId(orderId);
            _counter = 0;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nxt")
            {
                if (_counter != _contractors.Count - 1)
                {
                    _counter++;
                }
                else
                {
                    _counter = 0;
                }
            }
            else if (message.Data == "prv")
            {
                if (_counter != 0)
                {
                    _counter--;
                }
                else
                {
                    _counter = _contractors.Count - 1;
                }

            }
            else if (message.Data == "bck")
            {
                context.State = new ClientOrdersMenuState(_messageId);
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            
        }
    }
}
