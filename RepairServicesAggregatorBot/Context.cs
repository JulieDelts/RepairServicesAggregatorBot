﻿using RepairServicesAggregatorBot.Bot.States;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.SystemStates;

namespace RepairServicesAggregatorBot.Bot
{
    public class Context
    {
        public int Id { get; set; }

        public long ChatId { get; set; }

        public int RoleId { get; set; }

        public AbstractState State { get; set; }

        public Context()
        {
            State = new MockState();
        }

        public void HandleMessage(Update update, ITelegramBotClient botClient)
        {
            State.HandleMessage(this, update, botClient);
        }

        public void HandleCallbackQuery(Update update, ITelegramBotClient botClient)
        {
            State.HandleCallbackQuery(this, update, botClient);
        }

        public void ReactInBot(ITelegramBotClient botClient)
        {
            State.ReactInBot(this, botClient);
        }
    }
}
