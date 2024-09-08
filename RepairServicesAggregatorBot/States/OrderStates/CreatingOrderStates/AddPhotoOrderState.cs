using RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates;
using RepairServicesProviderBot.Core.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingOrderStates
{
    public class AddPhotoOrderState : AbstractState
    {
        public OrderInputModel OrderInputModel { get; set; }

        private bool _isPhotoError;

        public AddPhotoOrderState(OrderInputModel orderInputModel)
        {
            OrderInputModel = orderInputModel;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (message.Photo.Length > 0)
            {
                OrderInputModel.Photo = message.Photo.Last().FileId;

                context.State = new CompleteAddOrderState(OrderInputModel);
            }
            else
            {
                _isPhotoError = true;
            }
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isPhotoError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Photo error");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Send nudes");
            }
        }
    }
}
