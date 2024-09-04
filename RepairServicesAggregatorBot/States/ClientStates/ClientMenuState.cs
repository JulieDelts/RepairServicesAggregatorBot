using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.SystemStates.RegisteringUser;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ClientStates
{
    public class ClientMenuState: AbstractState
    {
        public UserInputModel UserInputModel { get; set; }

        public ClientMenuState(UserInputModel userInputModel)
        {
            UserInputModel = userInputModel;
        }

        public override void HandleMessage(Context context, Update update)
        {
            //var message = update.Message;

        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            var msg = await botClient.SendPhotoAsync(new ChatId(context.ChatId), InputFile.FromUri(new Uri(@"https://www.allrecipes.com/thmb/pENwa46VDqWGvYkycgTqHxr4xfo=/0x512/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/48727-Mikes-homemade-pizza-DDMFS-beauty-4x3-BG-2974-a7a9842c14e34ca699f3b7d7143256cf.jpg")));

            var qwe = msg.Photo.Last().FileId; //Возможно, это успех

            var message = await botClient.SendStickerAsync(new ChatId(context.ChatId), InputFile.FromFileId("CAACAgIAAxkBAAEIQAJm2KQmQ9lgITGWr0VCxCV2EKpFpgACDlkAAmm6yEpSDNwNc75gtzYE"));

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Создать заказ", "ord"),
                        InlineKeyboardButton.WithCallbackData("Заказы", "crtOrds")
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

                }
            );

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Меню пользователя", replyMarkup:keyboard);
        }
    }
}
