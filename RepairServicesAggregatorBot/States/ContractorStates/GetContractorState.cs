using RepairServicesProviderBot.BLL;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.Core.OutputModels;
using System.Text.RegularExpressions;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class GetContractorState: AbstractState
    {
        private bool _isIdError;

        private int _messageId;

        public GetContractorState(int messageId)
        {
            _messageId = messageId;
        }

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (IsIdValid(message.Text))
            {
                try
                {
                    int id = Convert.ToInt32(message.Text);

                    UserService userService = new();

                    var user = userService.GetUserById(id);

                    ContractorWithServiceTypesOutputModel contractorWithServiceTypesOutputModel = new()
                    {
                        Name = user.Name,
                        Phone = user.Phone,
                        Email = user.Email
                    };

                    ContractorService contractorService = new();

                    double? contractorRating = contractorService.GetContractorRating(id);

                    contractorWithServiceTypesOutputModel.Rating = contractorRating;

                    ServiceTypeService serviceTypeService = new();

                    var serviceTypes = serviceTypeService.GetContractorServiceTypesById(id);

                    contractorWithServiceTypesOutputModel.ServiceTypes = serviceTypes;

                    context.State = new ContractorServicesInfoState(contractorWithServiceTypesOutputModel);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    _isIdError = true;
                }
            }
            else
            {
                _isIdError = true;
            }
        }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isIdError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "ID введен некорректно.");
            }
            else
            {
                await botClient.DeleteMessageAsync(new ChatId(context.ChatId), _messageId);

                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите ID сотрудника:");
            }
        }

        private bool IsIdValid(string id)
        {
            return Regex.IsMatch(id, @"[0-9]+");
        }
    }
}
