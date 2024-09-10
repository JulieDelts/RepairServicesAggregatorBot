﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using RepairServicesProviderBot.BLL;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesAggregatorBot.Bot.States.SystemStates;
using RepairServicesAggregatorBot.Bot.States.AdminStates;
using RepairServicesAggregatorBot.Bot.States.ClientStates;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.OrderStates.CreatingBaseOrderStates
{
    public class CompleteAddOrderState : AbstractState
    {
        public OrderInputModel OrderInputModel { get; set; }

        private int _messageId;

        public CompleteAddOrderState(OrderInputModel order) 
        {
            OrderInputModel = order;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "bck")
            {
                context.State = new ClientMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            OrderService orderService = new OrderService();

            var orderId = orderService.AddOrder(OrderInputModel);

            var order = orderService.GetOrderById(orderId);

            var orderDescription = $"ID заказа: {order.Id}\nОписание: {order.OrderDescription}\nАдрес: {order.Address}\nДата создания: {order.Date}\n";

            await botClient.SendTextMessageAsync(new ChatId(context.ChatId), $"Добавление заказа завершено.\n{orderDescription}");

            AdminService adminService = new AdminService();

            var admins = adminService.GeAllAdmins();

            if (admins.Count > 0)
            {
                foreach (var admin in admins)
                {
                    await botClient.SendTextMessageAsync(new ChatId(admin.ChatId), $"Поступил новый заказ. Id: {order.Id}. Его можно посмотреть в меню новых заказов.");
                }
            }

            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "bck")
                }
            });

            var message = await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Назад к меню:", replyMarkup: keyboard);

            _messageId = message.MessageId;
        }
    }
}
