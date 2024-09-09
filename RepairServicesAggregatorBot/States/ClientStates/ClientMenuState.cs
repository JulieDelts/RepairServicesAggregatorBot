using RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ClientStates
{
    public class ClientMenuState : AbstractState
    {
        private int _messageId;

        public ClientMenuState(int messageId = 0)
        {
            _messageId = messageId;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "prf")
            {
                context.State = new UserProfileMenuState(_messageId);
            }
            else if (message.Data == "admhlp")
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Да, это пиздец, сочувствую!");
            }
            else if (message.Data == "ord")
            {
                context.State = new AddDescriptionOrderState(_messageId);
            }
            else if (message.Data == "srvs")
            {
                context.State = new AvailableServiceTypesState(_messageId);
            }
            else if (message.Data == "ords")
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
            //var msg = await botClient.SendPhotoAsync(new ChatId(context.ChatId), InputFile.FromUri(new Uri(@"https://www.allrecipes.com/thmb/pENwa46VDqWGvYkycgTqHxr4xfo=/0x512/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/48727-Mikes-homemade-pizza-DDMFS-beauty-4x3-BG-2974-a7a9842c14e34ca699f3b7d7143256cf.jpg")));

            //var qwe = msg.Photo.Last().FileId; //Возможно, это успех

            //await botClient.SendStickerAsync(new ChatId(context.ChatId), InputFile.FromFileId("CAACAgIAAxkBAAEIQAJm2KQmQ9lgITGWr0VCxCV2EKpFpgACDlkAAmm6yEpSDNwNc75gtzYE"));

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Создать заказ", "ord"),
                    InlineKeyboardButton.WithCallbackData("Заказы", "ords")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Профиль", "prf"),
                    InlineKeyboardButton.WithCallbackData("Доступные услуги", "srvs")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Поплакаться админу", "admhlp")
                }

            });

            if (_messageId == 0)
            {
                var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Меню пользователя", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню пользователя", replyMarkup: keyboard);
            }
        }
    }
}
