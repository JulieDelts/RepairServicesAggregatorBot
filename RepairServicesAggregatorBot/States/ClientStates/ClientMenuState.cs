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
