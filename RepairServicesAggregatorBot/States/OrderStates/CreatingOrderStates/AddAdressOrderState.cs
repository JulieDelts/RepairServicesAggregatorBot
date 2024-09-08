using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.BLL;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using System.Text.RegularExpressions;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates
{
    public class AddAdressOrderState : AbstractState
    {
        public OrderInputModel OrderInputModel { get; set; }

        private bool _isAddressError;

        public AddAdressOrderState(OrderInputModel orderInputModel)
        {
            OrderInputModel = orderInputModel;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsAddressValid(message.Text))
            {
                OrderInputModel.Address = message.Text;

                context.State = new CompleteAddOrderState(OrderInputModel);
            }
            else
            {
                _isAddressError = true;
            }
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isAddressError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Описание введено некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите адрес для предоставления услуги:");
            }
        }

        private bool IsAddressValid(string address)
        {
            return Regex.IsMatch(address, @"^[0-9а-яА-ЯёЁ\s.,]+");
        }
    }
}
