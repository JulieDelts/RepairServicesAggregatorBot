using RepairServicesAggregatorBot.Bot.States.SystemStates.UpdatingContractorServiceType;
using RepairServicesProviderBot.Core.InputModels;
using RepairServicesProviderBot.Core.OutputModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class ContractorServiceTypeState : AbstractState
    {
        private List<ContractorServiceTypeOutputModel> _contractorServiceTypes;

        private int _messageId;

        private int _counter;

        public ContractorServiceTypeState(int messageId, List<ContractorServiceTypeOutputModel> contractorServiceTypes)
        {
            _messageId = messageId;

            _counter = 0;

            _contractorServiceTypes = contractorServiceTypes;
        }

        public override async void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.CallbackQuery;

            if (message.Data == "nxt")
            {
                if (_counter != _contractorServiceTypes.Count - 1)
                {
                    _counter++;
                }
                else
                {
                    _counter = 0;
                }
            }
            else if (message.Data == "prv")
            {
                if (_counter != 0)
                {
                    _counter--;
                }
                else
                {
                    _counter = _contractorServiceTypes.Count - 1;
                }
            }
            else if (message.Data == "upd")
            {
                ContractorServiceTypeInputModel contractorServiceTypeInputModel = new();

                contractorServiceTypeInputModel.Id = _contractorServiceTypes[_counter].Id;

                contractorServiceTypeInputModel.UserId = context.Id;

                context.State = new StartUpdateContractorServiceTypeState(_messageId, contractorServiceTypeInputModel);
            }
            else if (message.Data == "dlt")
            {
                ContractorServiceTypeInputModel contractorServiceTypeInputModel = new();

                contractorServiceTypeInputModel.Id = _contractorServiceTypes[_counter].Id;

                contractorServiceTypeInputModel.UserId = context.Id;

                context.State = new DeleteContractorServiceType(_messageId, contractorServiceTypeInputModel);

            }
            else if (message.Data == "bck")
            {
                context.State = new ContractorServiceTypesMenuState(_messageId);
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Неверная команда.");
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_contractorServiceTypes.Count == 0)
            {
                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                       InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, "У вас нет услуг.", replyMarkup: keyboard);
            }
            else if (_contractorServiceTypes.Count == 1)
            {
                string serviceTypeDescription = $"Услуги сотрудника:\n{_contractorServiceTypes[_counter].ServiceTypeDescription}\nСтоимость: {_contractorServiceTypes[_counter].Cost}";

                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Изменить", "upd"),
                        InlineKeyboardButton.WithCallbackData("Удалить", "dlt")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад", "bck")
                    }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, serviceTypeDescription, replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
            else
            {
                string serviceTypeDescription = $"Услуги сотрудника:\n{_contractorServiceTypes[_counter].ServiceTypeDescription}\nСтоимость: {_contractorServiceTypes[_counter].Cost}";

                InlineKeyboardMarkup keyboard = new(
                new[]
                {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Изменить", "upd"),
                    InlineKeyboardButton.WithCallbackData("Удалить", "dlt"),

                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("⬅️", "prv"),
                    InlineKeyboardButton.WithCallbackData("Назад", "bck"),
                    InlineKeyboardButton.WithCallbackData("➡️", "nxt")
                }
                });

                var message = await botClient.EditMessageTextAsync(new ChatId(context.ChatId), _messageId, serviceTypeDescription, replyMarkup: keyboard);

                _messageId = message.MessageId;
            }
        }
    }
}
