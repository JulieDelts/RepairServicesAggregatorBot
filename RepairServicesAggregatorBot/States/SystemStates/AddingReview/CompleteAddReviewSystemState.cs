using RepairServicesAggregatorBot.Bot.States.ClientStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingReview
{
    public class CompleteAddReviewSystemState : AbstractState
    {
        public ReviewInputModel ReviewInputModel { get; set; }

        private int _messageId;

        public CompleteAddReviewSystemState(ReviewInputModel reviewInputModel)
        {
            ReviewInputModel = reviewInputModel;

            _messageId = 0;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new ClientOrdersMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            ReviewService reviewService = new();

            reviewService.AddReview(ReviewInputModel);

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Добавление отзыва завершено.");

            InlineKeyboardMarkup keyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Назад к меню заказов:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
