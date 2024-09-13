using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Text.RegularExpressions;

namespace RepairServicesAggregatorBot.Bot.States.SystemStates.AddingReview
{
    public class GetReviewDescriptionSystemState: AbstractState
    {
        public ReviewInputModel ReviewInputModel { get; set; }

        private bool _isDescriptionError;

        public GetReviewDescriptionSystemState(ReviewInputModel reviewInputModel)
        {
            ReviewInputModel = reviewInputModel;

            _isDescriptionError = false;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsDescriptionValid(message.Text))
            {
                ReviewInputModel.ReviewDescription = message.Text;

                context.State = new CompleteAddReviewSystemState(ReviewInputModel);
            }
            else if (message.Text == "no")
            {
                context.State = new CompleteAddReviewSystemState(ReviewInputModel);
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
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите описание к отзыву или 'no', чтобы перейти к следующему этапу:");
            }
        }

        private bool IsDescriptionValid(string description)
        {
            return Regex.IsMatch(description, @"^[а-яА-ЯёЁ\s]+");
        }
    }
}
