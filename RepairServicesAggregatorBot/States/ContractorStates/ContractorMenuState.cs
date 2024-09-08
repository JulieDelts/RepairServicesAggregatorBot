﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ContractorMenuState : AbstractState
    {
        private int _messageId;

        public ContractorMenuState(int messageId = 0)
        {
            _messageId = messageId;
        }

        public override async void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "prf")
            {
                context.State = new UserProfileMenuState(_messageId);
            }
            else if (message.Data == "srvtp")
            {
                //
            }
            //else if (message.Data == "cntrctr")
            //{
            //    context.State = new AdminContractorsMenuState(_messageId);
            //}
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Услуги", "srvtp"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Профиль", "prf"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Рейтинг", "ststcs"),
                },
            });

            if (_messageId == 0)
            {
                var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Меню сотрудника", replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "Меню струдника", replyMarkup: keyboard);
            }

        }
    }
}
