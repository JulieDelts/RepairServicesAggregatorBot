using Telegram.Bot;
using Telegram.Bot.Types;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using System.Text.RegularExpressions;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates
{
    public class AddDescriptionOrderState : AbstractState
    {
        public OrderInputModel OrderInputModel { get; set; }

        private bool _isDescriptionError;

        private int _messageId;

        public AddDescriptionOrderState(int messageId) 
        {
            OrderInputModel = new OrderInputModel();
            
            _isDescriptionError = false;

            _messageId = messageId;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsDescriptionValid(message.Text))
            {
                OrderInputModel.OrderDescription = message.Text;

                OrderInputModel.Date = message.Date.ToShortDateString();

                OrderInputModel.StatusId = 0;

                OrderInputModel.ClientId = context.Id;

                context.State = new AddAdressOrderState(OrderInputModel);
            }
            else
            {
                _isDescriptionError = true;
            }
        }
        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isDescriptionError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Описание введено некорректно.");
            }
            else
            {
                await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите описание заказа:");
            }
        }

        private bool IsDescriptionValid(string description)
        {
            return Regex.IsMatch(description, @"^[0-9а-яА-ЯёЁ\s.,]+");
        }
    }
}
