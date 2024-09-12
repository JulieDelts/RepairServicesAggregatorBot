using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class StartExecutingOrderContractorState : AbstractState
    {
        private UnassignedOrderOutputModel _order;

        private UserService _userService;

        public StartExecutingOrderContractorState(UnassignedOrderOutputModel order)
        {
            _order = order;

            _userService = new UserService();
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var client = _userService.GetUserById(_order.ClientId);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Вы теперь выполняете этот заказ:\n{_order.OrderDescription} по адресу {_order.Address}\nКлиент {client.Name} номер {client.Phone} почта {client.Email}");

            context.State = new ContractorMenuState();

            context.State.ReactInBot(context, botClient);
        }
    }
}
