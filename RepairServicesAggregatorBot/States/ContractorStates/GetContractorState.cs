using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairServicesAggregatorBot.Bot.States.ServiceTypeStates;
using RepairServicesProviderBot.BLL;
using RepairServicesProviderBot.Core.InputModels;
using Telegram.Bot.Types;
using Telegram.Bot;
using RepairServicesProviderBot.Core.OutputModels;

namespace RepairServicesAggregatorBot.Bot.States.ContractorStates
{
    public class GetContractorState: AbstractState
    {
        private bool _isIdError;

        public override void HandleMessage(Context context, Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (!string.IsNullOrEmpty(message.Text))
            {
                try
                {
                    int id = Convert.ToInt32(message.Text);

                    UserService userService = new UserService();

                    var user = userService.GetUserById(id);

                    ContractorWithServiceTypesOutputModel contractorWithServiceTypesOutputModel = new ContractorWithServiceTypesOutputModel()
                    {
                        Name = user.Name,
                        Phone = user.Phone,
                        Email = user.Email
                    };

                    ContractorService contractorService = new ContractorService();

                    double contractorRating = contractorService.GetContractorRating(id);

                    contractorWithServiceTypesOutputModel.Rating = contractorRating;

                    ServiceTypeService serviceTypeService = new ServiceTypeService();

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

        public override void HandleCallbackQuery(Context context, Update update, ITelegramBotClient botClient)
        { }

        public override async void ReactInBot(Context context, ITelegramBotClient botClient)
        {
            if (_isIdError)
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "ID введен некорректно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(new ChatId(context.ChatId), "Введите ID сотрудника:");
            }
        }
    }
}
