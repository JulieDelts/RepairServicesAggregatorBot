using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingReview
{
    public class StartAddReviewState: AbstractState
    {
        public ReviewInputModel ReviewInputModel { get; set; }

        private bool _isRatingError;

        private int _messageId;

        public StartAddReviewState(int messageId, int orderId)
        {
            ReviewInputModel = new();

            _isRatingError = false;

            _messageId = messageId;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var ratingCheck = int.TryParse(message.Text, out int result);

            if (ratingCheck && result >= 1 && result <= 10)
            {
                ReviewInputModel.Rating = result;

               context.State = new GetReviewDescriptionState(ReviewInputModel);
            }
            else
            {
                _isRatingError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isRatingError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Оценка введена некорректно.");
            }
            else
            {
                await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите оценку услуги от 1 до 10:");
            }
        }
    }
}
